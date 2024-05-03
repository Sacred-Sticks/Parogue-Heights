using Kickstarter.DependencyInjection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    [RequireComponent(typeof(UIDocument))]
    public class Score : MonoBehaviour
    {
        [SerializeField] private StyleSheet stylesheet;

        private Rigidbody body;
        
        private static Label scoreLabel;
        private Label heightLabel;

        private const string rootStr = "root";
        private const string containerStr = "container";
        private const string scoreStr = "score";
        private const string heightStr = "height";

        #region UnityEvents
        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            root.styleSheets.Add(stylesheet);
            root.AddToClassList(rootStr);
            BuildDocument(root);
        }

        private void Start()
        {
            body = Registry.Get<Rigidbody>(RegistryStrings.PlayerRigidbody);
        }

        private void Update()
        {
            heightLabel.text = $"{body.position.y.ToString("0.")} m";
        }
        #endregion

        private void BuildDocument(VisualElement root)
        {
            var container = root.CreateChild<VisualElement>(containerStr);
            scoreLabel = container.CreateChild<Label>(scoreStr);
            scoreLabel.text = "0";
            heightLabel = container.CreateChild<Label>(heightStr);
            heightLabel.text = "0";
        }
    
        public static void ModifyScore(int modification)
        {
            scoreLabel.text = (int.Parse(scoreLabel.text) + modification).ToString();
        }
    }
}
