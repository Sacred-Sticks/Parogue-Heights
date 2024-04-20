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

        [SerializeField] private Texture2D background;

        private string gameplayStr = "Gameplay";
        private const string rootStr = "root";
        private const string titleStr = "title";
        private const string playButtonStr = "play_button";
        private const string peacefulButtonStr = "peaceful_button";
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
            root.style.backgroundImage = new StyleBackground(background);

            var title = root.CreateChild<Label>(titleStr);
            title.text = "Parogue Heights";

            var playButton = root.CreateChild<Button>(playButtonStr);
            playButton.text = "Play";
            playButton.clickable.clicked += () => sceneLoader.LoadSceneGroup("Gameplay");

            var peacefulButton = root.CreateChild<Button>(peacefulButtonStr);
            peacefulButton.text = "Peaceful Mode";
            peacefulButton.clickable.clicked += () => sceneLoader.LoadSceneGroup("Peaceful Mode");

            var toolsButton = root.CreateChild<Button>(toolsButtonStr);
            toolsButton.text = "Tools";
            toolsButton.clickable.clicked += () =>
            {
                Close();
                Registry.Get<SpellsMenu>("Tools_Menu").Open();
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
            quitButton.clickable.clicked += () => Application.Quit();
        }
    }
}
