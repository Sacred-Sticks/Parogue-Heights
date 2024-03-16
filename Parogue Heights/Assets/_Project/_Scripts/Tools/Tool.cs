using UnityEngine;

namespace Parogue_Heights
{
    [CreateAssetMenu(fileName = "New Tool", menuName = "Parogue Heights/Inventory/Tool")]
    public class Tool : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite itemIcon;
        [SerializeField] private int pickupCount;

        public string ItemName => itemName;
        public Sprite ItemIcon => itemIcon;
        public int PickupCount => pickupCount;
    }
}
