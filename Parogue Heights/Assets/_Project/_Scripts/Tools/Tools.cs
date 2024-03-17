using UnityEngine;

namespace Parogue_Heights
{
    public interface ITool
    {
        public static ToolType GetToolType(ITool tool)
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

        public int Uses { get; }

        public void GainUses();
        public void OnActivateBegin();
        public void OnActivateEnd();

    }

    public class Trampoline : ITool
    {
        public Trampoline()
        {
            Uses = InitialUses;
            _trampolinePrefab = Resources.Load<GameObject>("Prefabs/Platforms/Trampoline");
            _platformMask = Resources.Load<LayerMaskData>("Objects/Layer Masks/Platform Layers");
        }

        public int Uses { get; private set; }

        private int InitialUses = 5;
        private const float range = 10f;

        private GameObject trampolineHologram;

        // Constants
        private GameObject _trampolinePrefab;
        private LayerMaskData _platformMask;

        #region Tool
        public void GainUses()
        {
            Uses += InitialUses;
            Debug.Log($"Gained {InitialUses} uses for Trampoline. Total uses: {Uses}");
        }
        
        public void OnActivateBegin() 
        {
            var cameraTransform = Camera.main.transform;
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (!Physics.Raycast(ray, out var hit, range, _platformMask.Mask))
                return;
            trampolineHologram = Object.Instantiate(_trampolinePrefab, hit.point, Quaternion.identity);
        }

        public void OnActivateEnd() 
        {
            Uses--;
            trampolineHologram.AddComponent<TrampolinePlatform>();
        }
        #endregion
    }
}
