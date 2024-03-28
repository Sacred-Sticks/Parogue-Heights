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
        private Label scoreElement;

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

        private void Update()
        {
            CheckScore();
        }
        #endregion

        private void BuildDocument(VisualElement root)
        {
            scoreElement = root.CreateChild<Label>(scoreStr);
            scoreElement.text = "0";
        }
    
        private void CheckScore()
        {
            if (body == null)
                return;
            if (body.position.y > highScore)
            {
                highScore = body.position.y;
                scoreElement.text = highScore.ToString("0");
            }
        }
    }
}
