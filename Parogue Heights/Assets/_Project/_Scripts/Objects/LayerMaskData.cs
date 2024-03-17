using UnityEngine;

namespace Parogue_Heights
{
    [CreateAssetMenu(fileName = "LayerMaskData", menuName = "Parogue Heights/LayerMaskData")]
    public class LayerMaskData : ScriptableObject
    {
        [SerializeField] private LayerMask _layerMask;

        public LayerMask Mask => _layerMask;
    }
}
