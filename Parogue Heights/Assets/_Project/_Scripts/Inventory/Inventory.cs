using Kickstarter.DependencyInjection;
using Kickstarter.Inputs;
using Kickstarter.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace Parogue_Heights
{
    public class Inventory : Singleton<Inventory>, IInputReceiver, IInventory
    {
        [Inject] private IInventoryHUD inventoryHUD;

        [SerializeField] private FloatInput inventoryCycleInput;

        private int activeSlotIndex = 0;
        private readonly List<InventorySlot> inventorySlots = new List<InventorySlot>();

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
            if (inventorySlots.Count == 0)
                return;
            int formerIndex = activeSlotIndex;
            activeSlotIndex += direction;
            activeSlotIndex = activeSlotIndex % inventorySlots.Count;
            if (activeSlotIndex < 0)
                activeSlotIndex = inventorySlots.Count - 1;
            inventoryHUD.ActivateSlot(activeSlotIndex, formerIndex);
        }

        #region Inventory
        public void CollectTool(ITool tool)
        {
            foreach (var inventorySlot in inventorySlots)
            {
                if (inventorySlot.Tool.GetType() != tool.GetType())
                    continue;
                inventorySlot.GainCount();
                return;
            }
            var slot = new InventorySlot(tool);
            inventoryHUD.AddSlot(slot);
            inventorySlots.Add(slot);
        }

        public void RemoveSlot(InventorySlot slot)
        {

        }
        #endregion
    }

    public interface IInventory
    {
        public void CollectTool(ITool tool);
        public void RemoveSlot(InventorySlot slot);
    }
}
