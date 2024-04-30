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

        private const string rootStr = "root";
        private const string titleStr = "title";
        private const string playButtonStr = "play_button";
        private const string optionsButtonStr = "options_button";
        private const string spellsButtonStr = "spells_button";
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

            var optionsButton = root.CreateChild<Button>(optionsButtonStr);
            optionsButton.text = "Options";
            optionsButton.clickable.clicked += () =>
            {
                Close();
                Registry.Get<OptionsMenu>("Options_Menu").Open();
            };

            var spellsButton = root.CreateChild<Button>(spellsButtonStr);
            spellsButton.text = "Spells";
            spellsButton.clickable.clicked += () =>
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
