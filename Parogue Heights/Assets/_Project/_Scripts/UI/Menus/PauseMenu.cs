using Kickstarter.Bootstrapper;
using Kickstarter.DependencyInjection;
using Kickstarter.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    public class PauseMenu : Menu, IDependencyProvider
    {
        [Provide] private Menu pauseMenu => this;
        [Inject] private SceneLoader sceneLoader;

        private const string containerStr = "container";
        private const string gamePausedStr = "gamePaused";
        private const string resumeStr = "resume";
        private const string buttonsStr = "buttons";
        private const string optionsStr = "options";
        private const string spellsStr = "spells";
        private const string controlsStr = "controls";
        private const string quitStr = "quit";

        #region UnityEvents
        private void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            BuildDocument();
            Close();
        }
        #endregion

        protected override void BuildDocument()
        {
            if (root == null)
                return;
            root.Clear();
            root.styleSheets.Add(styleSheet);

            var container = root.CreateChild<VisualElement>(containerStr);

            var gamePaused = container.CreateChild<Label>(gamePausedStr );
            gamePaused.text = "Game Paused";

            var resumeButton = container.CreateChild<Button>(resumeStr);
            resumeButton.text = "Resume";
            resumeButton.clickable.clicked += () => 
            {
                Close();
            };

            var buttons = container.CreateChild<VisualElement>(buttonsStr);

            var optionsButton = buttons.CreateChild<Button>(optionsStr);
            optionsButton.text = "Options";
            optionsButton.clickable.clicked += () =>
            {
                Registry.Get<Menu>("Options_Menu").Open();
                base.Close();
            };

            var spellsButton = buttons.CreateChild<Button>(spellsStr);
            spellsButton.text = "Spells";
            spellsButton.clickable.clicked += () =>
            {
                Registry.Get<Menu>("Tools_Menu").Open();
                base.Close();
            };

            var controlsButton = buttons.CreateChild<Button>(controlsStr);
            controlsButton.text = "Controls";
            controlsButton.clickable.clicked += () =>
            {
                Registry.Get<Menu>("Controls_Menu").Open();
                base.Close();
            };

            var quitButton = container.CreateChild<Button>(quitStr);
            quitButton.text = "Quit to Menu";
            quitButton.clickable.clicked += () =>
            {
                sceneLoader.LoadSceneGroup("Main Menu");
                Close();
            };
        }

        public override void Open()
        {
            base.Open();
            CursorLockManager.SetCursorLock(false);
            Time.timeScale = 0;
        }

        public override void Close()
        {
            base.Close();
            CursorLockManager.SetCursorLock(true);
            Time.timeScale = 1;
        }
    }
}
