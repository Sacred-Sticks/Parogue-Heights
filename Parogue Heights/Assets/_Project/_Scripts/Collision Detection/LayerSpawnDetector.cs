using Kickstarter.DependencyInjection;
using UnityEngine;

namespace Parogue_Heights
{
    public class LayerSpawnDetector : MonoBehaviour
    {
        [Inject] private ILayerSpawner layerSpawner;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == layerSpawner.GameObject)
                layerSpawner.SpawnLayerGroup();
        }
    }
}
