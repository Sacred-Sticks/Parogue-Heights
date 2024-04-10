using UnityEngine;

namespace Parogue_Heights
{
    [CreateAssetMenu(fileName = "New Spell Data", menuName = "Parogue Heights/Spell")]
    public class SpellData : ScriptableObject
    {
        [SerializeField] private ISpell.SpellType toolType;
        [SerializeField] private string toolDescription;
        [SerializeField] private Texture2D toolSprite;
        [SerializeField] private Color inventoryCountColor;

        public ISpell.SpellType ToolType => toolType;
        public string ToolDescription => toolDescription;
        public Texture2D ToolSprite => toolSprite;
        public Color InventoryCountColor => inventoryCountColor;
    }
}
