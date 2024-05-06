using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    [RequireComponent(typeof(UIDocument))]
    public class MenuWallpaper : MonoBehaviour
    {
        [SerializeField] private Texture2D background;

        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            root.style.backgroundImage = new StyleBackground(background);
        }

        private void OnValidate()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            if (root == null)
                return;
            root.style.backgroundImage = new StyleBackground(background);
        }
    }
}
