using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Parogue_Heights
{
    public sealed class HookshotSpell : ISpell
    {
        public HookshotSpell()
        {
            Uses = initialUses;
            body = Registry.Get<Rigidbody>(RegistryStrings.PlayerRigidbody);
            _platformMask = Resources.Load<LayerMaskData>(ResourcePaths.PlatformMask);
            particleMediators = new[]
            {
                Registry.Get<ParticlesMediator>(RegistryStrings.LeftHookshot),
                Registry.Get<ParticlesMediator>(RegistryStrings.RightHookshot),
            };
            reticleMediator = Registry.Get<ParticlesMediator>(RegistryStrings.HookshotReticle);
            lineRendererMediator = Registry.Get<LineRendererMediator>(RegistryStrings.HookshotLineRenderer);
            lineRendererMediator.WithInitialPoint(body.transform);
            lineRendererMediator.WithInitialOffset(Vector3.up * height / 2);
            moveController = body.GetComponentInChildren<IMoveController>();
        }

        private bool hookshotActive;
        private bool hookshotPulling;

        private const int initialUses = 3;
        private const float range = 30f;
        private const float forceStrength = 20f;
        private const float stoppingDistance = 0.5f;
        private const float height = 1.5f;
        private readonly Vector3 offset = Vector3.up * height;
        private readonly Rigidbody body;
        private readonly LayerMaskData _platformMask;
        private readonly ParticlesMediator[] particleMediators;
        private readonly ParticlesMediator reticleMediator;
        private readonly LineRendererMediator lineRendererMediator;
        private readonly IMoveController moveController;

        private async void MoveInDirection(Vector3 goalPosition)
        {
            hookshotActive = true;
            hookshotPulling = true;
            var newVelocity = (goalPosition - body.position).normalized * forceStrength;
            moveController.CanMove = false;
            while (hookshotActive)
            {
                if (body == null)
                    return;

                var velocity = newVelocity - body.velocity;
                var currentPosition = body.position + body.transform.up * height;
                var direction = goalPosition - currentPosition;
                float x = direction.x * newVelocity.x;
                float y = direction.y * newVelocity.y;
                float z = direction.z * newVelocity.z;
                if (x < 0 || y < 0 || z < 0)
                    hookshotPulling = false;
                if (!hookshotPulling)
                    velocity = -body.velocity;
                body.AddForce(velocity, ForceMode.VelocityChange);
                await Task.Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime));
            }
        }

        #region Tool
        private int uses;
        public int Uses
        {
            get => uses;
            set
            {
                uses = value;
                InventoryHUD.ChangeSlotCount(InventorySlot, Uses);
            }
        }
        public InventorySlot InventorySlot { get; set; }

        public void GainUses() => Uses += initialUses;

        public void OnActivateBegin()
        {
            if (hookshotActive)
                return;
            var cameraTransform = Camera.main.transform;
            var ray = new Ray(body.position + offset, cameraTransform.forward);
            if (!Physics.Raycast(ray, out var hit, range, _platformMask.Mask))
                return;
            body.useGravity = false;
            MoveInDirection(hit.point);
            foreach (var particleMediator in particleMediators)
                particleMediator.Play();
            reticleMediator.transform.position = hit.point;
            reticleMediator.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            reticleMediator.Play();
        }

        public void OnActivateEnd()
        {
            if (!hookshotActive)
                return;
            body.useGravity = true;
            hookshotActive = false;
            moveController.CanMove = true;
            ISpell.LowerUses(this);
            foreach (var particleMediator in particleMediators)
                particleMediator.Stop();
            reticleMediator.Stop();
        }

        public void OnSlotActive()
        {
            var cameraTransform = Camera.main.transform;
            var ray = new Ray(body.position + offset, cameraTransform.forward);
            if (Physics.Raycast(ray, out var hit, range, _platformMask.Mask))
                lineRendererMediator.RenderLine(hit.point);
        }
        #endregion
    }
}
