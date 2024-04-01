using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Parogue_Heights
{
    public sealed class BouncePad : ITool
    {
        public BouncePad()
        {
            Uses = InitialUses;
            _trampolinePrefab = Resources.Load<GameObject>(ResourcePaths.BouncePad);
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
                hologram.transform.position = hit.point;
                hologram.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
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
            hologram = UnityEngine.Object.Instantiate(_trampolinePrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            HologramFollowCenter(cameraTransform);
        }

        public void OnActivateEnd()
        {
            if (!isPlacing || hologram == null)
                return;
            isPlacing = false;
            hologram.AddComponent<BouncePadPlatform>();
            hologram = null;
            ITool.LowerUses(this);
        }
        #endregion
    }
}
