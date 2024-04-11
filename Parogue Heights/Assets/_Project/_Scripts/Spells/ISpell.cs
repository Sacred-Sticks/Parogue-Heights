namespace Parogue_Heights
{
    public interface ISpell
    {
        public static SpellType GetToolType(ISpell tool)
        {
            return tool switch
            {
                BounceSpell _ => SpellType.Bounce,
                HookshotSpell _ => SpellType.Hookshot,
                JumpSpell _ => SpellType.Jump,
                _ => default,
            };
        }

        public static void LowerUses(ISpell tool)
        {
            tool.Uses--;
            if (tool.Uses <= 0)
                Inventory.Instance.RemoveSlot(tool.InventorySlot);
        }

        public enum SpellType
        {
            Bounce,
            Hookshot,
            Jump,
        }

        public int Uses { get; set; }
        public InventorySlot InventorySlot { get; set; }

        public void GainUses();
        public void OnActivateBegin();
        public void OnActivateEnd();
    }
}
