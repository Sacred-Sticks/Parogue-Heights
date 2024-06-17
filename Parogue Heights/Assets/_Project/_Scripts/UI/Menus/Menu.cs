using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] protected StyleSheet styleSheet;

        protected VisualElement root;

        private bool isOpen;

        public virtual void Open()
        {
            root.style.display = DisplayStyle.Flex;
            isOpen = true;
        }

        public virtual void Close()
        {
            root.style.display = DisplayStyle.None;
            isOpen = false;
        }

        public void ToggleMenu()
        {
            System.Action action = isOpen ? Close : Open;
            action();
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
                return;
            root = GetComponent<UIDocument>().rootVisualElement;
            BuildDocument();
        }

        protected abstract void BuildDocument();
    }
}
