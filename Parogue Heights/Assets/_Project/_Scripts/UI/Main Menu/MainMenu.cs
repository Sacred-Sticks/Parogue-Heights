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

        private string gameplayStr = "Gameplay";
        private const string rootStr = "root";
        private const string titleStr = "title";
        private const string playButtonStr = "play_button";
        private const string quitButtonStr = "quit_button";

        #region UnityEvents
        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            root.styleSheets.Add(stylesheet);
            root.AddToClassList(rootStr);
            BuildMenu(root);
        }
        #endregion

        private void BuildMenu(VisualElement root)
        {
            var title = root.CreateChild<Label>(titleStr);
            title.text = "Parogue Heights";
            var playButton = root.CreateChild<Button>(playButtonStr);
            playButton.text = "Play";
            playButton.clickable.clicked += StartGame;
            
            var quitButton = root.CreateChild<Button>(quitButtonStr);
            quitButton.text = "Quit";
            quitButton.clickable.clicked += QuitGame;
        }

        private void StartGame()
        {
            sceneLoader.LoadSceneGroup(gameplayStr);
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}
