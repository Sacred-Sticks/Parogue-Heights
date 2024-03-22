using Kickstarter.DependencyInjection;
using UnityEngine;

namespace Parogue_Heights
{
    public class LayerSpawner : MonoBehaviour, IDependencyProvider, ILayerSpawner
    {
        [Provide] private ILayerSpawner _layerSpawner => this;
        [Inject] private IGenerator generator;

        private const int layerGroupSize = 10;

        public GameObject GameObject => gameObject;

        public void SpawnLayerGroup()
        {
            for (int i = 0; i < layerGroupSize; i++)
                generator.GenerateLayer();
            transform.position = generator.Offset;
        }
    }

    public interface ILayerSpawner
    {
        public GameObject GameObject { get; }
        public void SpawnLayerGroup();
    }
}
