namespace Parogue_Heights
{
    public class InventorySlot
    {
        public InventorySlot(Tool tool)
        {
            _tool = tool;
        }

        private Tool _tool;
        public Tool Tool => _tool;
        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                if (_count < 1)
                {
                    
                }
            }
        }
        public Tool.ToolType ToolType => Tool.GetToolType(Tool);

        public void GainCount()
        {
            Tool.GainUses();
        }
    }

    public interface IInventorySlot
    {
        public Tool Tool { get; }
        public int Count { get; set; }
        public Tool.ToolType ToolType { get; }
        public void GainCount();
    }
}
