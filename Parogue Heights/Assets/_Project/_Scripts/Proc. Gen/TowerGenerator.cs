using Kickstarter.DependencyInjection;
using Kickstarter.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parogue_Heights
{
    public class TowerGenerator : MonoBehaviour, IGenerator, IDependencyProvider
    {
        [Provide] private IGenerator generator => this;

        [SerializeField, EnumData(typeof(IGenerator.LayerHeight)),] private Layer[] layers;

        private int layersGenerated;
        private IGenerator.LayerHeight currentLayerCategory = IGenerator.LayerHeight.Initial;
        public Vector3 Offset { get; private set; }

        // Constants and Readonly
        private const int initialLayers = 20;
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
            int multiple = Random.Range(0, 4);
            var angles = new Vector3(0, 90 * multiple, 0);
            var position = transform.position + Offset;
            Instantiate(selectedVariant, position, Quaternion.Euler(angles), transform);
            Offset += Vector3.up * layer.LayerHeight;
            IncrementLayer();
        }

        public void GenerateInitialLayers()
        {
            for (int i = 0; i < initialLayers; i++)
                GenerateLayer();
        }
        #endregion

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
        public Vector3 Offset { get; }

        public void GenerateLayer();
        public void GenerateInitialLayers();

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
