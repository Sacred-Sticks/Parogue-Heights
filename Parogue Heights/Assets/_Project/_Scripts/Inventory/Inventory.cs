using Kickstarter.DependencyInjection;
using Kickstarter.Inputs;
using Kickstarter.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Parogue_Heights
{
    public class Inventory : Singleton<Inventory>, IInputReceiver, IInventory
    {
        [Inject] private IInventoryHUD inventoryHUD;

        [SerializeField] private FloatInput inventoryCycleInput;
        [SerializeField] private FloatInput useToolInput;

        private int activeSlotIndex = 0;
        private readonly List<InventorySlot> inventorySlots = new List<InventorySlot>();

        #region InputHandler
        public void RegisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            inventoryCycleInput.RegisterInput(OnInventoryCycleInputChange, playerIdentifier);
            useToolInput.RegisterInput(OnUseToolInputChange, playerIdentifier);
        }

        public void DeregisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            inventoryCycleInput.DeregisterInput(OnInventoryCycleInputChange, playerIdentifier);
            useToolInput.DeregisterInput(OnUseToolInputChange, playerIdentifier);
        }

        private void OnInventoryCycleInputChange(float input)
        {
            if (input == 0)
                return;
            CycleInventory((int)input);
        }

        private void OnUseToolInputChange(float input)
        {
            if (inventorySlots.Count == 0)
                return;

            Action action = input == 1 ? 
                inventorySlots[activeSlotIndex].Tool.OnActivateBegin : 
                inventorySlots[activeSlotIndex].Tool.OnActivateEnd;
            action?.Invoke();

            if (inventorySlots[activeSlotIndex].Tool.Uses <= 0)
                RemoveSlot(inventorySlots[activeSlotIndex]);
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

        private void RemoveSlot(InventorySlot slot)
        {
            inventorySlots.Remove(slot);
            inventoryHUD.RemoveSlot(slot);
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
        #endregion
    }

    public interface IInventory
    {
        public void CollectTool(ITool tool);
    }
}
