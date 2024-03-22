using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Parogue_Heights
{
    public interface ITool
    {
        public static ToolType GetToolType(ITool tool)
        {
            return tool switch
            {
                BouncePad _ => ToolType.Trampoline,
                _ => default,
            };
        }

        public enum ToolType
        {
            Trampoline,
        }

        public int Uses { get; }
        public InventorySlot InventorySlot { set; }

        public void GainUses();
        public void OnActivateBegin();
        public void OnActivateEnd();

    }

    public sealed class BouncePad : ITool
    {
        public BouncePad()
        {
            Uses = InitialUses;
            _trampolinePrefab = Resources.Load<GameObject>(ResourcePaths.BouncePad);
            _platformMask = Resources.Load<LayerMaskData>(ResourcePaths.PlatformMask);
        }

        private int InitialUses = 5;
        private const float range = 25f;

        private GameObject hologram;
        private bool isPlacing;

        // Constants
        private GameObject _trampolinePrefab;
        private LayerMaskData _platformMask;


        private void LowerUses()
        {
            Uses--;
            if (Uses <= 0)
                Inventory.Instance.RemoveSlot(InventorySlot);
        }

        private async void HologramFollowCenter(Transform cameraTransform)
        {
            isPlacing = true;
            while (isPlacing)
            {
                var ray = new Ray(cameraTransform.position, cameraTransform.forward);
                if (!Physics.Raycast(ray, out var hit, range, _platformMask.Mask))
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
        public int Uses { get; private set; }
        public InventorySlot InventorySlot { private get; set; }

        public void GainUses()
        {
            Uses += InitialUses;
        }

        public void OnActivateBegin()
        {
            var cameraTransform = Camera.main.transform;
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (!Physics.Raycast(ray, out var hit, range, _platformMask.Mask))
                return;
            hologram = UnityEngine.Object.Instantiate(_trampolinePrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            HologramFollowCenter(cameraTransform);
        }

        public void OnActivateEnd()
        {
            if (hologram == null)
                return;
            isPlacing = false;
            hologram.AddComponent<BouncePadPlatform>();
            hologram = null;
            LowerUses();
        }
        #endregion
    }
}
