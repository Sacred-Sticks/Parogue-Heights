using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    public struct SlotElement
    {
        private VisualElement slotElement;
        private Label count;

        private const string slotStr = "slot";
        private const string countStr = "count";
        private const string activeStr = "active";

        public SlotElement(VisualElement container, InventorySlot slot, SpellData data)
        {
            slotElement = container.CreateChild<VisualElement>(slotStr);
            slotElement.style.backgroundImage = new StyleBackground(data.ToolSprite);
            count = slotElement.CreateChild<Label>(countStr);
            count.style.color = data.InventoryCountColor;
            count.text = slot.Count.ToString();
        }

        public void ChangeCount(int newCount)
        {
            count.text = newCount.ToString();
        }

        public void Activate()
        {
            slotElement.AddToClassList(activeStr);
        }

        public void Deactivate()
        {
            slotElement.RemoveFromClassList(activeStr);
        }

        public void RemoveFromHierarchy()
        {
            slotElement.RemoveFromHierarchy();
        }
    }
}
