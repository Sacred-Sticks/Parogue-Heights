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
        
        private float highScore = 0;
        private static Label scoreElement;

        private const string rootStr = "root";
        private const string scoreStr = "score";

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
        #endregion

        private void BuildDocument(VisualElement root)
        {
            scoreElement = root.CreateChild<Label>(scoreStr);
            scoreElement.text = "0";
        }
    
        public static void ModifyScore(int modification)
        {
            scoreElement.text = (int.Parse(scoreElement.text) + modification).ToString();
        }
    }
}
