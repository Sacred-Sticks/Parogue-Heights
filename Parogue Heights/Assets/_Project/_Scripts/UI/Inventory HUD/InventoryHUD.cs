using Kickstarter.DependencyInjection;
using Kickstarter.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    [RequireComponent(typeof(UIDocument))]
    public class InventoryHUD : MonoBehaviour, IInventoryHUD, IDependencyProvider
    {
        [Provide] private IInventoryHUD inventoryHUD => this;

        [SerializeField] private StyleSheet styleSheet;
        [Space]
        [SerializeField, EnumData(typeof(ITool.ToolType))] private Sprite[] _toolSprites;

        // Constants
        private VisualElement container;
        private const string rootStr = "root";
        private const string containerStr = "container";
        private VisualElement root;
        private readonly List<InventorySlot> slots = new List<InventorySlot>();
        private static readonly Dictionary<InventorySlot, SlotElement> slotsHUD = new Dictionary<InventorySlot, SlotElement>();
        public static readonly Dictionary<ITool.ToolType, Sprite> toolSprites = new Dictionary<ITool.ToolType, Sprite>();

        #region UnityEvents
        private void Awake()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            toolSprites.Clear();
            toolSprites.LoadDictionary(_toolSprites);
        }

        private void Start()
        {
            root.styleSheets.Add(styleSheet);
            BuildDocument();
        }
        #endregion

        private void BuildDocument()
        {
            root.AddToClassList(rootStr);
            container = root.CreateChild<VisualElement>(containerStr);

            ActivateSlot(0);
        }

        #region InventoryHUD
        public void ActivateSlot(int newSlotIndex, int formerSlotIndex = 0)
        {
            if (slotsHUD.Count == 0)
                return;
            if (slots.Count == 0)
                return;
            if (slotsHUD.Count > formerSlotIndex)
                slotsHUD[slots[formerSlotIndex]].Deactivate();
            slotsHUD[slots[newSlotIndex]].Activate();
        }

        public void AddSlot(InventorySlot slot)
        {
            var slotElement = new SlotElement(container, slot);
            slots.Add(slot);
            slotsHUD.Add(slot, slotElement);
            if (slots.Count == 1)
                ActivateSlot(0);
        }

        public void RemoveSlot(InventorySlot slot)
        {
            int index = slots.IndexOf(slot);
            if (slots.Count > 1 && index < slots.Count - 1)
                ActivateSlot(index + 1, index);
            slotsHUD[slot].RemoveFromHierarchy();
            slotsHUD.Remove(slot);
            slots.Remove(slot);
        }

        public static void ChangeSlotCount(InventorySlot slot, int count)
        {
            if (slot != null)
                slotsHUD[slot].ChangeCount(count);
        }
        #endregion
    }

    public interface IInventoryHUD
    {
        public void ActivateSlot(int newSlotIndex, int formerSlotIndex);
        public void AddSlot(InventorySlot slot);
        public void RemoveSlot(InventorySlot slot);
    }
}
