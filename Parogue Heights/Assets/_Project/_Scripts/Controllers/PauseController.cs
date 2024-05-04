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
            pauseInput.RegisterInput(OnPauseInputChange, playerIdentifier);
        }

        public void DeregisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            pauseInput.RegisterInput(OnPauseInputChange, playerIdentifier);
        }
        #endregion

        private void OnPauseInputChange(float input)
        {
            if (input == 0)
                return;
            sceneLoader.LoadSceneGroup("Main Menu");
        }
    }
}
