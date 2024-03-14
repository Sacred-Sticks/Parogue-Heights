using Kickstarter.DependencyInjection;
using Kickstarter.Inputs;
using UnityEngine;

namespace Parogue_Heights
{
    public class Inventory : MonoBehaviour, IInputReceiver
    {
        [Inject] private IInventoryHUD inventoryHUD;

        [SerializeField] private FloatInput inventoryCycleInput;

        private int activeSlotIndex = 0;

        #region InputHandler
        public void RegisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            inventoryCycleInput.RegisterInput(OnInventoryCycleInputChange, playerIdentifier);
        }

        public void DeregisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            inventoryCycleInput.DeregisterInput(OnInventoryCycleInputChange, playerIdentifier);
        }

        public void OnInventoryCycleInputChange(float input)
        {
            if (input == 0)
                return;
            CycleInventory((int)input);
        }
        #endregion

        private void CycleInventory(int direction)
        {
            int formerIndex = activeSlotIndex;
            activeSlotIndex += direction;
            activeSlotIndex = activeSlotIndex % inventoryHUD.SlotCount;
            if (activeSlotIndex < 0)
                activeSlotIndex = inventoryHUD.SlotCount - 1;
            inventoryHUD.ActivateSlot(activeSlotIndex, formerIndex);
        }
    }
}
