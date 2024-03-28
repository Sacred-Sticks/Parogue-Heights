using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Parogue_Heights
{
    public sealed class Hookshot : ITool
    {
        public Hookshot()
        {
            Uses = initialUses;
            body = Registry.Get<Rigidbody>(RegistryStrings.PlayerRigidbody);
            _platformMask = Resources.Load<LayerMaskData>(ResourcePaths.PlatformMask);
        }

        private bool hookshotActive;

        private const int initialUses = 3;
        private const float range = 25f;
        private const float radius = 1;
        private const float forceStrength = 10f;
        private const float stoppingDistance = 0.5f;
        private const float height = 2;
        private readonly Rigidbody body;
        private readonly LayerMaskData _platformMask;

        private async void MoveInDirection(Vector3 goalPosition)
        {
            hookshotActive = true;
            var newVelocity = (goalPosition - body.position).normalized * forceStrength;
            while (hookshotActive)
            {
                if (body == null)
                    hookshotActive = false;
                var velocity = newVelocity - body.velocity;
                var currentPosition = body.position + body.transform.up * height;
                if (Vector3.SqrMagnitude(currentPosition - goalPosition) < stoppingDistance * stoppingDistance)
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
            var cameraTransform = Camera.main.transform;
            var ray = new Ray(body.position, cameraTransform.forward);
            if (!Physics.SphereCast(ray, radius, out var hit, range, _platformMask.Mask))
                return;
            body.useGravity = false;
            MoveInDirection(hit.point);
        }

        public void OnActivateEnd()
        {
            body.useGravity = true;
            hookshotActive = false;
            ITool.LowerUses(this);
        }
        #endregion
    }
}
