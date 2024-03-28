namespace Parogue_Heights
{
    public class InventorySlot
    {
        public InventorySlot(ITool tool)
        {
            _tool = tool;
        }

        private ITool _tool;
        public ITool Tool => _tool;
        public int Count => Tool.Uses;
        public ITool.ToolType ToolType => ITool.GetToolType(Tool);

        public void GainCount()
        {
            Tool.GainUses();
        }
    }

    public interface IInventorySlot
    {
        public ITool Tool { get; }
        public int Count { get; set; }
        public ITool.ToolType ToolType { get; }
        public void GainCount();
    }
}
