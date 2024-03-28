namespace Parogue_Heights
{
    public interface ITool
    {
        public static ToolType GetToolType(ITool tool)
        {
            return tool switch
            {
                BouncePad _ => ToolType.BouncePad,
                Hookshot _ => ToolType.Hookshot,
                JetBoots _ => ToolType.JetBoots,
                _ => default,
            };
        }

        public static void LowerUses(ITool tool)
        {
            tool.Uses--;
            if (tool.Uses <= 0)
                Inventory.Instance.RemoveSlot(tool.InventorySlot);
        }

        public enum ToolType
        {
            BouncePad,
            Hookshot,
            JetBoots,
        }

        public int Uses { get; set; }
        public InventorySlot InventorySlot { get; set; }

        public void GainUses();
        public void OnActivateBegin();
        public void OnActivateEnd();
    }
}
