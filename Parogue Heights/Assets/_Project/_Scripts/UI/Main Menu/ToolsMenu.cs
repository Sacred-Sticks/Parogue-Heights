using Kickstarter.DependencyInjection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    [RequireComponent(typeof(UIDocument))]
    public class ToolsMenu : Menu
    {
        [Inject] private MainMenu mainMenu;
        
        [SerializeField, EnumData(typeof(ITool.ToolType))] private ToolData[] _toolData;

        // Constants
        private const string containerStr = "container";
        private const string scrollerStr = "scroller";
        private const string scrollerBackdropStr = "scroller_backdrop";
        private const string scrollerButtonStr = "scroller_button";
        private const string closeButtonStr = "close_button";
        private const string toolContainerStr = "tool_container";
        private const string toolDataContainerStr = "tool_data_container";
        private const string toolImageStr = "tool_image";
        private const string toolNameStr= "tool_name";
        private const string toolDescriptionStr = "tool_description";

        #region UnityEvents
        private void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            BuildDocument();
            Close();
        }

        private void OnEnable()
        {
            Registry.Register("Tools_Menu", this);
        }

        private void OnDisable()
        {
            Registry.Deregister("Tools_Menu");
        }
        #endregion

        protected override void BuildDocument()
        {
            if (root == null)
                return;
            root.Clear();
            root.styleSheets.Add(styleSheet);

            var scrollView = root.CreateChild<ScrollView>(containerStr);
            scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
            var container = scrollView.Q("unity-content-container");
            foreach (var toolData in _toolData)
                AddTool(toolData, container);

            var scroller = scrollView.verticalScroller;
            scroller.Q("unity-dragger").AddToClassList(scrollerStr);
            scroller.Q("unity-tracker").AddToClassList(scrollerBackdropStr);
            scroller.Q("unity-low-button").AddToClassList(scrollerButtonStr);
            scroller.Q("unity-high-button").AddToClassList(scrollerButtonStr);

            var button = root.CreateChild<Button>(closeButtonStr);
            button.text = "Close";
            button.clickable.clicked += Close;
        }

        private void AddTool(ToolData toolData, VisualElement container)
        {
            var toolContainer = container.CreateChild<VisualElement>(toolContainerStr);
            var image = toolContainer.CreateChild<VisualElement>(toolImageStr);
            image.style.backgroundImage = new StyleBackground(toolData.ToolSprite);
            var toolDataElement = toolContainer.CreateChild<VisualElement>(toolDataContainerStr);
            toolDataElement.CreateChild<Label>(toolNameStr).text = toolData.ToolType.ToString();
            toolDataElement.CreateChild<Label>(toolDescriptionStr).text = toolData.ToolDescription;
        }

        public override void Close()
        {
            base.Close();
            if (mainMenu != null)
                mainMenu.Open();
        }
    }
}
