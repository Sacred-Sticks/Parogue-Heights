using UnityEngine.UIElements;

namespace Parogue_Heights
{
    public struct SlotElement
    {
        private VisualElement slotElement;

        public SlotElement(VisualElement container, InventorySlot slot)
        {
            slotElement = container.CreateChild<VisualElement>(InventoryHUD.slotStr);
            slotElement.style.backgroundImage = new StyleBackground(InventoryHUD.toolSprites[slot.ToolType]);
        }

        public void Activate()
        {
            slotElement.AddToClassList(InventoryHUD.activeStr);
        }

        public void Deactivate()
        {
            slotElement.RemoveFromClassList(InventoryHUD.activeStr);
        }

        public void RemoveFromHierarchy()
        {
            slotElement.RemoveFromHierarchy();
        }
    }
}
