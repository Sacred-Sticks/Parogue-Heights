using System;

namespace Parogue_Heights
{
    public interface ITool
    {
        public int GainAmount { get; }

        public enum ToolType
        {
            Trampoline,
        }

        public static ToolType GetToolType(ITool tool)
        {
            return tool switch
            {
                Trampoline _ => ToolType.Trampoline,
                _ => throw new ArgumentOutOfRangeException(nameof(tool), tool, null)
            };
        }
    }

    public class Trampoline : ITool
    {
        public int GainAmount { get; } = 5;
    }
}
