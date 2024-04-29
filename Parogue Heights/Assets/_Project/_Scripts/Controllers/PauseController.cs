using UnityEngine;
using Kickstarter.Inputs;
using Kickstarter.DependencyInjection;
using Kickstarter.Bootstrapper;

namespace Parogue_Heights
{
    public class PauseController : MonoBehaviour, IInputReceiver
    {
        [Inject] private SceneLoader sceneLoader;

        [SerializeField] private FloatInput pauseInput;

        #region InputHandler
        public void RegisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            pauseInput.RegisterInput((f) => sceneLoader.LoadSceneGroup("Main Menu"), playerIdentifier);
        }

        public void DeregisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            pauseInput.RegisterInput((f) => sceneLoader.LoadSceneGroup("Main Menu"), playerIdentifier);
        }
        #endregion
    }
}
