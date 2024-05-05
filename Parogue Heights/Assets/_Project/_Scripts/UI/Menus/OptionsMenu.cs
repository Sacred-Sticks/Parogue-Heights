using Kickstarter.DependencyInjection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    public class OptionsMenu : Menu
    {
        [Inject] private Menu primaryMenu;

        private const string containerStr = "container";
        private const string titleStr = "title";
        private const string optionsStr = "options";
        private const string sensitivityStr = "sensitivity";
        private const string closeButtonStr = "close_button";

        private Slider sensitivitySlider;

        #region UnityEvents
        private void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            BuildDocument();
            Close();
        }

        private void OnEnable()
        {
            Registry.Register("Options_Menu", this);
        }

        private void OnDisable()
        {
            Registry.Deregister("Options_Menu");
        }
        #endregion

        protected override void BuildDocument()
        {
            if (root == null)
                return;
            root.Clear();
            root.styleSheets.Add(styleSheet);

            var container = root.CreateChild<VisualElement>(containerStr);
            var title = container.CreateChild<Label>(titleStr);
            title.text = "Options Menu";

            var options = container.CreateChild<ScrollView>(optionsStr).Q("unity-content-container");

            sensitivitySlider = options.CreateChild<Slider>(sensitivityStr);
            sensitivitySlider.label = "Sensitivity";
            sensitivitySlider.lowValue = 0.0f;
            sensitivitySlider.highValue = 2.0f;
            sensitivitySlider.value = Settings.Sensitivity;

            var closeButton = container.CreateChild<Button>(closeButtonStr);
            closeButton.text = "Close";
            closeButton.clickable.clicked += Close;
        }

        public override void Close()
        {
            Settings.Sensitivity = sensitivitySlider.value;
            primaryMenu?.Open();
            base.Close();
        }

        public override void Open()
        {
            base.Open();
            sensitivitySlider.value = Settings.Sensitivity;
        }
    }
}
