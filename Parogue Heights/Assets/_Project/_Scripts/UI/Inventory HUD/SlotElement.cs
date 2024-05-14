using UnityEngine.UIElements;
using Kickstarter.Extensions;

namespace Parogue_Heights
{
    public struct SlotElement
    {
        private VisualElement slotContainer;
        private VisualElement slotElement;
        private Label count;
        private Label spellName;

        private const string slotContainerStr = "slotContainer";
        private const string slotStr = "slot";
        private const string countStr = "count";
        private const string spellNameStr = "spellName";
        private const string activeStr = "active";

        public SlotElement(VisualElement container, InventorySlot slot, SpellData data)
        {
            slotContainer = container.CreateChild<VisualElement>(slotContainerStr);
            slotElement = slotContainer.CreateChild<VisualElement>(slotStr);
            slotElement.style.backgroundImage = new StyleBackground(data.ToolSprite);
            count = slotElement.CreateChild<Label>(countStr);
            count.style.color = data.LabelColor;
            count.text = slot.Count.ToString();
            spellName = slotContainer.CreateChild<Label>(spellNameStr);
            spellName.text = data.ToolType.ToString();
            spellName.style.color = data.LabelColor;
        }

        public void ChangeCount(int newCount)
        {
            count.text = newCount.ToString();
        }

        public void Activate()
        {
            slotElement.AddToClassList(activeStr);
            spellName.AddToClassList(activeStr);
        }

        public void Deactivate()
        {
            slotElement.RemoveFromClassList(activeStr);
            spellName.RemoveFromClassList(activeStr);
        }

        public void RemoveFromHierarchy()
        {
            slotContainer.RemoveFromHierarchy();
        }
    }
}
