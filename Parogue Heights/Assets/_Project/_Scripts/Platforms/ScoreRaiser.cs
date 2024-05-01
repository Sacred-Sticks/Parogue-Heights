using System.Collections.Generic;
using UnityEngine;

namespace Parogue_Heights
{
    [RequireComponent(typeof(Collider))]
    public class ScoreRaiser : MonoBehaviour, IPlatform
    {
        [SerializeField] private IGenerator.LayerHeight layerType;

        private static readonly Dictionary<IGenerator.LayerHeight, int> pointsPerLayerType = new Dictionary<IGenerator.LayerHeight, int>
        {
            {IGenerator.LayerHeight.Initial, 1 },
            {IGenerator.LayerHeight.Easy, 2},
            {IGenerator.LayerHeight.Medium, 5},
            {IGenerator.LayerHeight.Hard, 15},
            {IGenerator.LayerHeight.Impossible, 20},
        };
        
        #region UnityEvents
        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnEnable()
        {
            Registry.Register<IPlatform>(transform.position, this);
        }

        private void OnDisable()
        {
            Registry.Deregister(transform.position);
        }
        #endregion

        public GameObject GameObject => gameObject;

        public void OnPlayerEnter(Rigidbody body)
        {
            Score.ModifyScore(pointsPerLayerType[layerType]);
            Registry.Deregister(transform.position);
            Destroy(gameObject);
        }
    }
}
