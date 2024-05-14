using UnityEngine.UIElements;
using Kickstarter.Extensions;

namespace Parogue_Heights
{
    public class Credits : Menu
    {
        private const string downwardStr = "downward";
        private const string creditsStr = "credits";

        #region UnityEvents
        private void Start()
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

            var downwardContainer = root.CreateChild<VisualElement>(downwardStr);

            var credits = downwardContainer.CreateChild<Label>(creditsStr);
            credits.text = "A Game by Lucas Ackman";
        }
    }
}
