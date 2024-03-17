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
            _trampolinePrefab = Resources.Load<GameObject>("Trampoline");
        }

        public int Uses { get; private set; }

        private int InitialUses = 5;
        private const float range = 10f;

        private GameObject trampolineHologram;

        // Constants
        private GameObject _trampolinePrefab;

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
            if (!Physics.Raycast(ray, out var hit))
                return;
            trampolineHologram = Object.Instantiate(_trampolinePrefab, hit.point, Quaternion.identity);
            // Place Hologram of Trampoline at Cursor Raycast Position
        }

        public void OnActivateEnd() 
        {
            Uses--;
            // Transform Hologram into Trampoline
        }
        #endregion
    }
}
