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
        public void GainCount();
        public ITool.ToolType ToolType { get; }
    }
}
