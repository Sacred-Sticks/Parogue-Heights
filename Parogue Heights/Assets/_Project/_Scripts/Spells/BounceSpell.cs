using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Parogue_Heights
{
    public sealed class BounceSpell : ISpell
    {
        public BounceSpell()
        {
            Uses = InitialUses;
            _trampolinePrefab = Resources.Load<GameObject>(ResourcePaths.BounceRune);
            _platformMask = Resources.Load<LayerMaskData>(ResourcePaths.PlatformMask);
        }

        private GameObject hologram;
        private bool isPlacing;

        // Constants
        private const int InitialUses = 5;
        private const float range = 25f;
        private readonly GameObject _trampolinePrefab;
        private readonly LayerMaskData _platformMask;

        private async void HologramFollowCenter(Transform cameraTransform)
        {
            isPlacing = true;
            while (isPlacing)
            {
                if (cameraTransform == null)
                    return;
                var ray = new Ray(cameraTransform.position, cameraTransform.forward);
                if (!Physics.Raycast(ray, out var hit, range, _platformMask.Mask))
                {
                    await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
                    continue;
                }
                if (PlatformManager.IsWithinRadius(hit.point))
                {
                    await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
                    continue;
                }
                if (!CheckBorders(hit.point, -hit.normal))
                {
                    await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
                    continue;
                }
                hologram.transform.position = hit.point;
                hologram.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
            }
        }

        /// <summary>
        /// Check if the bouncepad is entirely on a platform
        /// </summary>
        /// <param name="hitPoint"></param>
        /// <returns>True if the full platform is above a floor tile, else false</returns>
        private bool CheckBorders(Vector3 hitPoint, Vector3 direction)
        {
            const float raycastRange = 0.1f;
            const float distance = 0.875f;
            var origin = hitPoint - direction * raycastRange / 4;

            var right = Vector3.Cross(direction, direction + Vector3.one).normalized;
            var forward = Vector3.Cross(direction, right);

            Vector3 raycastOrigin;
            raycastOrigin = origin + right * distance;
            if (!Physics.Raycast(raycastOrigin, direction, raycastRange, _platformMask.Mask))
                return false;
            raycastOrigin = origin - right * distance;
            if (!Physics.Raycast(raycastOrigin, direction, raycastRange, _platformMask.Mask))
                return false;
            raycastOrigin = origin + forward * distance;
            if (!Physics.Raycast(raycastOrigin, direction, raycastRange, _platformMask.Mask))
                return false;
            raycastOrigin = origin - forward * distance;
            if (!Physics.Raycast(raycastOrigin, direction, raycastRange, _platformMask.Mask))
                return false;

            float cornerDistance = Mathf.Sin(45) * distance;
            raycastOrigin = origin + right * cornerDistance + forward * cornerDistance;
            if (!Physics.Raycast(raycastOrigin, direction, raycastRange, _platformMask.Mask))
                return false;
            raycastOrigin = origin - right * cornerDistance + forward * cornerDistance;
            if (!Physics.Raycast(raycastOrigin, direction, raycastRange, _platformMask.Mask))
                return false;
            raycastOrigin = origin + right * cornerDistance - forward * cornerDistance;
            if (!Physics.Raycast(raycastOrigin, direction, raycastRange, _platformMask.Mask))
                return false;
            raycastOrigin = origin - right * cornerDistance - forward * cornerDistance;
            if (!Physics.Raycast(raycastOrigin, direction, raycastRange, _platformMask.Mask))
                return false;

            return true;
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

        public void GainUses()
        {
            Uses += InitialUses;
        }

        public void OnActivateBegin()
        {
            if (isPlacing)
                return;
            var cameraTransform = Camera.main.transform;
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (!Physics.Raycast(ray, out var hit, range, _platformMask.Mask))
                return;
            if (PlatformManager.IsWithinRadius(hit.point))
                return;
            if (!CheckBorders(hit.point, -hit.normal))
                return;
            hologram = UnityEngine.Object.Instantiate(_trampolinePrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            HologramFollowCenter(cameraTransform);
        }

        public void OnActivateEnd()
        {
            if (!isPlacing || hologram == null)
                return;
            isPlacing = false;
            hologram.AddComponent<BounceRune>();
            hologram = null;
            ISpell.LowerUses(this);
        }
        #endregion
    }
}
