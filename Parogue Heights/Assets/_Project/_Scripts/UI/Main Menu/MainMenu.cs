using Kickstarter.Bootstrapper;
using Kickstarter.DependencyInjection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    [RequireComponent(typeof(UIDocument))]
    public class MainMenu : MonoBehaviour
    {
        [Inject] private SceneLoader sceneLoader;

        [SerializeField] private StyleSheet stylesheet;

        private string gameplaySceneGroupStr = "Gameplay";
        private const string rootClassStr = "root";
        private const string playButtonClassStr = "play_button";

        #region UnityEvents
        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            root.styleSheets.Add(stylesheet);
            root.AddToClassList(rootClassStr);
            BuildMenu(root);
        }
        #endregion

        private void BuildMenu(VisualElement root)
        {
            var button = root.CreateChild<Button>(playButtonClassStr);
            button.text = "Play";
            button.clickable.clicked += StartGame;
        }

        private void StartGame()
        {
            sceneLoader.LoadSceneGroup(gameplaySceneGroupStr);
        }
    }
}
