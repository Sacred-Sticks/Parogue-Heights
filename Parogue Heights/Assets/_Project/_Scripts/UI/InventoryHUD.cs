using Kickstarter.DependencyInjection;
using UnityEngine;
using UnityEngine.UIElements;

namespace The_Endless_Ascent
{
    [RequireComponent(typeof(UIDocument))]
    public class InventoryHUD : MonoBehaviour, IInventoryHUD
    {
        [Provide] private IInventoryHUD inventoryHUD => this;

        [SerializeField] private StyleSheet styleSheet;

        private int activeSlotIndex;

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
        public void ActivateSlot(int slotIndex)
        {
            slots[activeSlotIndex].RemoveFromClassList(activeStr);
            activeSlotIndex = slotIndex;
            slots[activeSlotIndex].AddToClassList(activeStr);
        }
        #endregion
    }

    public interface IInventoryHUD
    {
        public void ActivateSlot(int slotIndex);
    }
}
