using UnityEngine;

namespace The_Endless_Ascent
{
    public class TowerGenerator : MonoBehaviour, IGenerator
    {
        [SerializeField, /*EnumData(typeof(IGenerator.LayerHeight)),*/] private Layer[] layers;

        private int layersGenerated;
        private IGenerator.LayerHeight currentLayerCategory;

        // Constants
        private const int initialLayers = 5;

        #region Generator
        public void GenerateInitialLayers()
        {
            for (int i = 0; i < initialLayers; i++)
            {
                GenerateLayer();
            }
        }

        public void GenerateLayer()
        {

        }
        #endregion

        [System.Serializable]
        private struct Layer
        {
            public GameObject[] layerVariants;
        }
    }

    public interface IGenerator
    {
        public void GenerateInitialLayers();
        public void GenerateLayer();

        public enum LayerHeight
        {
            Initial = 0,
            Easy = 10,
            Medium = 30,
            Hard = 70,
            Impossible = 150,
        }
    }
}
