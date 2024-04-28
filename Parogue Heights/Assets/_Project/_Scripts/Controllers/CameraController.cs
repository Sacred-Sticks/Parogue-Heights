using Cinemachine;
using Kickstarter.DependencyInjection;
using UnityEngine;

namespace Parogue_Heights
{
    public class CameraController : MonoBehaviour, IDependencyProvider
    {
        [Provide] private CinemachineVirtualCamera virtualCamera;
        [Provide] private Cinemachine3rdPersonFollow follow;

        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            follow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }
    }
}
