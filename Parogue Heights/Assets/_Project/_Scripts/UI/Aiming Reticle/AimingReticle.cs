using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    [RequireComponent(typeof(UIDocument))]
    public class AimingReticle : MonoBehaviour
    {
        [SerializeField] private StyleSheet stylesheet;
        [SerializeField] private Sprite reticleSprite;

        private const string containerStr = "container";
        private const string reticleStr = "reticle";

        #region UnityEvents
        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            BuildDocument(root);
        }

        private void OnValidate()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            BuildDocument(root);
        }
        #endregion

        private void BuildDocument(VisualElement root)
        {
            if (root == null)
                return;
            root.Clear();
            root.styleSheets.Add(stylesheet);

            var container = root.CreateChild<VisualElement>(containerStr);
            var reticle = container.CreateChild<VisualElement>(reticleStr);
            reticle.style.backgroundImage = new StyleBackground(reticleSprite);
        }
    }
}
