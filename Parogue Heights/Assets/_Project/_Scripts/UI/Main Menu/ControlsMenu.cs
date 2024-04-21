using Kickstarter.DependencyInjection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    [RequireComponent(typeof(UIDocument))]
    public class ControlsMenu : Menu
    {
        [Inject] private MainMenu mainMenu;

        [SerializeField] private Input[] inputs;
        [SerializeField] private Texture2D background;

        private const string headerStr = "header";
        private const string containerStr = "container";
        private const string closeButtonStr = "close_button";
        private const string controlStr = "control";
        private const string scrollerStr = "scroller";
        private const string scrollerBackdropStr = "scroller_backdrop";
        private const string scrollerButtonStr = "scroller_button";

        #region UnityEvents
        private void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            BuildDocument();
            Close();
        }

        private void OnEnable()
        {
            Registry.Register("Controls_Menu", this);
        }

        private void OnDisable()
        {
            Registry.Deregister("Controls_Menu");
        }
        #endregion

        protected override void BuildDocument()
        {
            if (root == null)
                return;
            root.Clear();
            root.styleSheets.Add(styleSheet);
            root.style.backgroundImage = new StyleBackground(background);

            root.CreateChild<Label>(headerStr).text = "Controls";
            var scrollView = root.CreateChild<ScrollView>(containerStr);
            var scroller = scrollView.verticalScroller;
            scroller.Q("unity-dragger").AddToClassList(scrollerStr);
            scroller.Q("unity-tracker").AddToClassList(scrollerBackdropStr);
            scroller.Q("unity-low-button").AddToClassList(scrollerButtonStr);
            scroller.Q("unity-high-button").AddToClassList(scrollerButtonStr);
            scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

            var container = scrollView.Q("unity-content-container");

            foreach (Input input in inputs)
            {
                var control = container.CreateChild<VisualElement>(controlStr);
                control.CreateChild<Label>().text = input.Name;
                control.CreateChild<Label>().text = input.InputKey;
            }

            var button = root.CreateChild<Button>(closeButtonStr);
            button.text = "Close";
            button.clickable.clicked += Close;
        }

        public override void Close()
        {
            base.Close();
            if (mainMenu != null)
                mainMenu.Open();
        }

        [System.Serializable]
        private struct Input
        {
            [SerializeField] private string name;
            [SerializeField] private string inputKey;

            public string Name => name;
            public string InputKey => inputKey;
        }
    }
}
