using UnityEngine;

namespace Parogue_Heights
{
    public abstract class Tool
    {
        public static ToolType GetToolType(Tool tool)
        {
            return tool switch
            {
                Trampoline _ => ToolType.Trampoline,
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

    public sealed class Trampoline : Tool
    {
        public Trampoline()
        {
            Uses = InitialUses;
            _trampolinePrefab = Resources.Load<GameObject>("Prefabs/Platforms/Trampoline");
            _platformMask = Resources.Load<LayerMaskData>("Objects/Layer Masks/Platform Layers");
        }

        private int InitialUses = 5;
        private const float range = 10f;

        private GameObject trampolineHologram;

        // Constants
        private GameObject _trampolinePrefab;
        private LayerMaskData _platformMask;

        protected override void LowerUses()
        {
            Uses--;
            if (Uses <= 0)
                Inventory.Instance.RemoveSlot(InventorySlot);
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
            trampolineHologram = Object.Instantiate(_trampolinePrefab, hit.point, Quaternion.identity);
        }

        public override void OnActivateEnd() 
        {
            trampolineHologram.AddComponent<TrampolinePlatform>();
            LowerUses();
        }
        #endregion
    }
}
