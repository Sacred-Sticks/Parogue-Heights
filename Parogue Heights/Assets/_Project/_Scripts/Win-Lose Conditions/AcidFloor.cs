using UnityEngine;
using System;
using Kickstarter.DependencyInjection;
using Kickstarter.Bootstrapper;

namespace Parogue_Heights
{
    public class AcidFloor : MonoBehaviour, ILoseCondition, IDependencyProvider
    {
        [Provide] private ILoseCondition loseCondition => this;
        [Inject] private SceneLoader sceneLoader;

        private string sceneGroupName = "Main Menu";

        private const float initialSpeed = 0f;
        private const float speedMultiplier = 0.5f;
        private readonly Func<float, float> formula = (x) => Mathf.Sqrt(x / 60) / speedMultiplier + initialSpeed;

        private float time;
        private float speed;

        #region UnityEvents
        private void Update()
        {
            time += Time.deltaTime;
            speed = formula(time);
            transform.position += Vector3.up * (speed * Time.deltaTime);
        }
        #endregion

        #region LoseCondition
        public GameObject GameObject => gameObject;

        public void Lose()
        {
            sceneLoader.LoadSceneGroup(sceneGroupName);
        }
        #endregion
    }
}
