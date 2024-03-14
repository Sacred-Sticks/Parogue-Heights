using Kickstarter.DependencyInjection;
using Kickstarter.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace The_Endless_Ascent
{
    public class TowerGenerator : MonoBehaviour, IGenerator, IDependencyProvider
    {
        [Provide] private IGenerator generator => this;

        [SerializeField, EnumData(typeof(IGenerator.LayerHeight)),] private Layer[] layers;

        private int layersGenerated;
        private IGenerator.LayerHeight currentLayerCategory = IGenerator.LayerHeight.Initial;
        private Vector3 offset;

        // Constants and Readonly
        private const int initialLayers = 5;
        private readonly Dictionary<IGenerator.LayerHeight, Layer> sortedPrefabs 
            = new Dictionary<IGenerator.LayerHeight, Layer>();

        #region UnityEvents
        private void Awake()
        {
            sortedPrefabs.LoadDictionary(layers);
        }

        private void Start()
        {
            GenerateInitialLayers();
        }
        #endregion

        #region Generator
        public void GenerateLayer()
        {
            var layer = sortedPrefabs[currentLayerCategory];
            var selectedVariant = layer.LayerVariants[Random.Range(0, layer.LayerVariants.Length)];
            Instantiate(selectedVariant, transform.position + offset, Quaternion.identity, transform);
            offset += Vector3.up * layer.LayerHeight;
            IncrementLayer();
        }
        #endregion

        private void GenerateInitialLayers()
        {
            for (int i = 0; i < initialLayers; i++)
                GenerateLayer();
        }

        private void IncrementLayer()
        {
            layersGenerated++;
            if (layersGenerated >= (int) currentLayerCategory)
            {
                currentLayerCategory = currentLayerCategory switch
                {
                    IGenerator.LayerHeight.Initial => IGenerator.LayerHeight.Easy,
                    IGenerator.LayerHeight.Easy => IGenerator.LayerHeight.Medium,
                    IGenerator.LayerHeight.Medium => IGenerator.LayerHeight.Hard,
                    IGenerator.LayerHeight.Hard => IGenerator.LayerHeight.Impossible,
                    _ => currentLayerCategory
                };
            }
        }

        [System.Serializable] private struct Layer
        {
            [SerializeField] private GameObject[] _layerVariants;
            [SerializeField] private float _layerHeight;

            public GameObject[] LayerVariants => _layerVariants;
            public float LayerHeight => _layerHeight;
        }
    }

    public interface IGenerator
    {
        public void GenerateLayer();

        public enum LayerHeight
        {
            Initial = 1,
            Easy = 9,
            Medium = 33,
            Hard = 65,
            Impossible,
        }
    }
}
