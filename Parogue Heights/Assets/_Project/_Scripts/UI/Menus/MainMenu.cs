using Kickstarter.Bootstrapper;
using Kickstarter.DependencyInjection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    public class MainMenu : Menu, IDependencyProvider
    {
        [Provide] private Menu mainMenu => this;
        [Inject] private SceneLoader sceneLoader;

        private const string rootStr = "root";
        private const string titleContainerStr = "title_container";
        private const string titleStr = "title";
        private const string playButtonStr = "play_button";
        private const string buttonsStr = "buttons";
        private const string spellsButtonStr = "spells_button";
        private const string menuButtonsStr = "menu_buttons";
        private const string optionsButtonStr = "options_button";
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

            var titleContainer = root.CreateChild<VisualElement>(titleContainerStr);

            var title = titleContainer.CreateChild<Label>(titleStr);
            title.text = "Parogue Heights";

            var playButton = root.CreateChild<Button>(playButtonStr);
            playButton.text = "Play";
            playButton.clickable.clicked += () => sceneLoader.LoadSceneGroup("Gameplay");

            var buttons = root.CreateChild<VisualElement>(buttonsStr);

            var spellsButton = buttons.CreateChild<Button>(spellsButtonStr);
            spellsButton.text = "Spells";
            spellsButton.clickable.clicked += () =>
            {
                Close();
                Registry.Get<SpellsMenu>("Tools_Menu").Open();
            };

            var menuButtons = buttons.CreateChild<VisualElement>(menuButtonsStr);

            var controlsButton = menuButtons.CreateChild<Button>(controlsButtonStr);
            controlsButton.text = "Controls";
            controlsButton.clickable.clicked += () =>
            {
                Close();
                Registry.Get<ControlsMenu>("Controls_Menu").Open();
            };

            var optionsButton = menuButtons.CreateChild<Button>(optionsButtonStr);
            optionsButton.text = "Options";
            optionsButton.clickable.clicked += () =>
            {
                Close();
                Registry.Get<OptionsMenu>("Options_Menu").Open();
            };

            var quitButton = buttons.CreateChild<Button>(quitButtonStr);
            quitButton.text = "Quit";
            quitButton.clickable.clicked += () => Application.Quit();
        }
    }
}
