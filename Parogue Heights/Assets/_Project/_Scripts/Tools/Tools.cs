using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Parogue_Heights
{
    public abstract class Tool
    {
        public static ToolType GetToolType(Tool tool)
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

        public int Uses { get; protected set; }
        public InventorySlot InventorySlot { protected get; set; }

        protected abstract void LowerUses();
        public abstract void GainUses();
        public abstract void OnActivateBegin();
        public abstract void OnActivateEnd();

    }

    public sealed class BouncePad : Tool
    {
        public BouncePad()
        {
            Uses = InitialUses;
            _trampolinePrefab = Resources.Load<GameObject>("Prefabs/Platforms/Bounce Pad");
            _platformMask = Resources.Load<LayerMaskData>("Objects/Layer Masks/Platform Layers");
        }

        private int InitialUses = 5;
        private const float range = 25f;

        private GameObject hologram;
        private bool isPlacing;

        // Constants
        private GameObject _trampolinePrefab;
        private LayerMaskData _platformMask;

        protected override void LowerUses()
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
        public override void GainUses()
        {
            Uses += InitialUses;
        }

        public override void OnActivateBegin()
        {
            var cameraTransform = Camera.main.transform;
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (!Physics.Raycast(ray, out var hit, range, _platformMask.Mask))
                return;
            hologram = UnityEngine.Object.Instantiate(_trampolinePrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            HologramFollowCenter(cameraTransform);
        }

        public override void OnActivateEnd()
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
