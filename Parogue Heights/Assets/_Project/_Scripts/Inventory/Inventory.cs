using Kickstarter.DependencyInjection;
using Kickstarter.Inputs;
using Kickstarter.Singleton;
using System;
using System.Collections;
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
        private bool canUseTool = true;

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
            if (!canUseTool && input == 1)
                return;

            if (inventorySlots.Count == 0)
                return;

            Action action = input == 1 ? 
                inventorySlots[activeSlotIndex].Tool.OnActivateBegin : 
                inventorySlots[activeSlotIndex].Tool.OnActivateEnd;
            action?.Invoke();

            if (activeSlotIndex >= inventorySlots.Count && activeSlotIndex > 0)
                CycleInventory(-1);

            if (input == 1)
                StartCoroutine(UseSpellCooldown());
        }
        #endregion

        private void CycleInventory(int direction)
        {
            if (inventorySlots.Count == 0)
            {
                activeSlotIndex = 0;
                return;
            }
            int formerIndex = activeSlotIndex;
            activeSlotIndex += direction;
            activeSlotIndex = activeSlotIndex % inventorySlots.Count;
            if (inventorySlots.Count > formerIndex)
                inventorySlots[formerIndex].Tool.OnActivateEnd();
            if (activeSlotIndex < 0)
                activeSlotIndex = inventorySlots.Count - 1;
            inventoryHUD.ActivateSlot(activeSlotIndex, formerIndex);
        }

        private IEnumerator UseSpellCooldown()
        {
            var cooldown = 0.125f;
            canUseTool = false;
            yield return new WaitForSeconds(cooldown);
            canUseTool = true;
        }

        #region Inventory
        public void RemoveSlot(InventorySlot slot)
        {
            inventorySlots.Remove(slot);
            inventoryHUD.RemoveSlot(slot);
        }

        public void CollectTool(ISpell tool)
        {
            foreach (var inventorySlot in inventorySlots)
            {
                if (inventorySlot.Tool.GetType() != tool.GetType())
                    continue;
                inventorySlot.GainCount();
                return;
            }
            var slot = new InventorySlot(tool);
            tool.InventorySlot = slot;
            inventoryHUD.AddSlot(slot);
            inventorySlots.Add(slot);
        }
        #endregion
    }

    public interface IInventory
    {
        public void RemoveSlot(InventorySlot slot);
        public void CollectTool(ISpell tool);
    }
}
