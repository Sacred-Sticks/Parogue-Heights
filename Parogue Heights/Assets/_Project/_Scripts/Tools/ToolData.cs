using UnityEngine;

namespace Parogue_Heights
{
    public abstract class ToolData
    {

    }

    public sealed class TrampolineData : ToolData
    {
        [SerializeField] private GameObject trampolinePrefab;
        public GameObject TrampolinePrefab => trampolinePrefab;
    }
}
