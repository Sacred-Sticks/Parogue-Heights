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
        private readonly Rigidbody body;
        private readonly LayerMaskData _platformMask;

        private async void MoveInDirection(Vector3 goalPosition)
        {
            hookshotActive = true;
            var newVelocity = (goalPosition - body.position).normalized * forceStrength;
            while (hookshotActive)
            {
                body.AddForce(newVelocity - body.velocity, ForceMode.VelocityChange);
                if (Vector3.SqrMagnitude(body.position - goalPosition) < 4f)
                    OnActivateEnd();
                await Task.Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime));
            }
        }

        #region Tool
        public int Uses { get; set; }
        public InventorySlot InventorySlot { get; set; }

        public void GainUses() => Uses += initialUses;

        public void OnActivateBegin()
        {
            var cameraTransform = Camera.main.transform;
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (!Physics.SphereCast(ray, radius, out var hit, range, _platformMask.Mask))
                return;
            body.useGravity = false;
            MoveInDirection(hit.point);
        }

        public void OnActivateEnd()
        {
            body.useGravity = true;
            hookshotActive = false;
        }
        #endregion
    }
}
