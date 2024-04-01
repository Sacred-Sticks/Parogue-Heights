using Kickstarter.Bootstrapper;
using Kickstarter.DependencyInjection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    [RequireComponent(typeof(UIDocument))]
    public class MainMenu : Menu, IDependencyProvider
    {
        [Provide] private MainMenu mainMenu => this;
        [Inject] private SceneLoader sceneLoader;

        private string gameplayStr = "Gameplay";
        private const string rootStr = "root";
        private const string titleStr = "title";
        private const string playButtonStr = "play_button";
        private const string toolsButtonStr = "tools_button";
        private const string controlsButtonStr = "controls_button";
        private const string quitButtonStr = "quit_button";

        #region UnityEvents
        private void Awake()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            BuildDocument();
        }
        #endregion

        protected override void BuildDocument()
        {
            if (root == null)
                return;
            root.styleSheets.Add(styleSheet);
            root.AddToClassList(rootStr);

            var title = root.CreateChild<Label>(titleStr);
            title.text = "Parogue Heights";
            var playButton = root.CreateChild<Button>(playButtonStr);
            playButton.text = "Play";
            playButton.clickable.clicked += StartGame;

            var toolsButton = root.CreateChild<Button>(toolsButtonStr);
            toolsButton.text = "Tools";
            toolsButton.clickable.clicked += () =>
            {
                Close();
                Registry.Get<ToolsMenu>("Tools_Menu").Open();
            };

            var controlsButton = root.CreateChild<Button>(controlsButtonStr);
            controlsButton.text = "Controls";
            controlsButton.clickable.clicked += () =>
            {
                Close();
                Registry.Get<ControlsMenu>("Controls_Menu").Open();
            };

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
