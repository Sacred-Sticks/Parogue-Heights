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
        [SerializeField, EnumData(typeof(Tool.ToolType))] private Sprite[] _toolSprites;

        // Constants
        private VisualElement container;
        private const string rootStr = "root";
        private const string containerStr = "container";
        private const string slotStr = "slot";
        private const string activeStr = "active";
        private VisualElement root;
        private readonly List<InventorySlot> slots = new List<InventorySlot>();
        private readonly Dictionary<InventorySlot, VisualElement> slotsHUD = new Dictionary<InventorySlot, VisualElement>();
        private readonly Dictionary<Tool.ToolType, Sprite> toolSprites = new Dictionary<Tool.ToolType, Sprite>();

        #region UnityEvents
        private void Awake()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
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
            slotsHUD[slots[formerSlotIndex]].RemoveFromClassList(activeStr);
            slotsHUD[slots[newSlotIndex]].AddToClassList(activeStr);
        }

        public void AddSlot(InventorySlot slot)
        {
            var slotElement = container.CreateChild<VisualElement>(slotStr);
            slotElement.style.backgroundImage = new StyleBackground(toolSprites[slot.ToolType]);
            slots.Add(slot);
            slotsHUD.Add(slot, slotElement);
        }

        public void RemoveSlot(InventorySlot slot)
        {
            slotsHUD[slot].RemoveFromHierarchy();
            slotsHUD.Remove(slot);
            slots.Remove(slot);
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
