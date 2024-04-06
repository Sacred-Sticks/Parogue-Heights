using UnityEngine;

namespace Parogue_Heights
{
    public class ToolSpawner : MonoBehaviour, IPlatform
    {
        private ParticleSystem[] particleSystems;

        #region UnityEvents
        private void Start()
        {
            Registry.Register<IPlatform>(transform.position, this);
            particleSystems = GetComponentsInChildren<ParticleSystem>();
        }
        #endregion

        private void ProvideTool()
        {
            foreach (var particleSystem in particleSystems)
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            var tool = ToolFactory.CreateRandomTool();
            Inventory.Instance.CollectTool(tool);
            Registry.Deregister(transform.position);
            GetComponent<Collider>().enabled = false;
        }

        #region Platform
        public GameObject GameObject => gameObject;

        public void OnPlayerEnter(Rigidbody body)
        {
            ProvideTool();
        }
        #endregion
    }
}
