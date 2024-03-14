using Kickstarter.DependencyInjection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Parogue_Heights
{
    [RequireComponent(typeof(UIDocument))]
    public class InventoryHUD : MonoBehaviour, IInventoryHUD, IDependencyProvider
    {
        [Provide] private IInventoryHUD inventoryHUD => this;
        public int SlotCount => slotCount;

        [SerializeField] private StyleSheet styleSheet;

        // Constants
        private const string rootStr = "root";
        private const string containerStr = "container";
        private const string slotStr = "slot";
        private const string activeStr = "active";
        private const int slotCount = 5;
        private VisualElement root;
        private VisualElement[] slots = new VisualElement[slotCount];

        #region UnityEvents
        private void Awake()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
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
            var container = root.CreateChild<VisualElement>(containerStr);

            for (int i = 0; i < slotCount; i++)
                slots[i] = container.CreateChild<VisualElement>(slotStr);

            ActivateSlot(0);
        }

        #region InventoryHUD
        public void ActivateSlot(int newSlotIndex, int formerSlotIndex = 0)
        {
            slots[formerSlotIndex].RemoveFromClassList(activeStr);
            slots[newSlotIndex].AddToClassList(activeStr);
        }
        #endregion
    }

    public interface IInventoryHUD
    {
        public int SlotCount { get; }

        public void ActivateSlot(int newSlotIndex, int formerSlotIndex);
    }
}
