namespace Parogue_Heights
{
    public class InventorySlot
    {
        public InventorySlot(ISpell tool)
        {
            _tool = tool;
        }

        private ISpell _tool;
        public ISpell Tool => _tool;
        public int Count => Tool.Uses;
        public ISpell.SpellType ToolType => ISpell.GetToolType(Tool);

        public void GainCount()
        {
            Tool.GainUses();
        }
    }

    public interface IInventorySlot
    {
        public ISpell Tool { get; }
        public int Count { get; set; }
        public ISpell.SpellType ToolType { get; }
        public void GainCount();
    }
}
