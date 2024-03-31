using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    public abstract class Menu : MonoBehaviour
    {
        protected VisualElement root;

        public virtual void Open()
        {
            root.style.display = DisplayStyle.Flex;
        }

        public virtual void Close()
        {
            root.style.display = DisplayStyle.None;
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
