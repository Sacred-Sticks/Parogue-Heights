using System;

namespace Parogue_Heights
{
    public interface ITool
    {
        public static ToolType GetToolType(ITool tool)
        {
            return tool switch
            {
                Trampoline _ => ToolType.Trampoline,
                _ => throw new ArgumentOutOfRangeException(nameof(tool), tool, null)
            };
        }

        public enum ToolType
        {
            Trampoline,
        }

        public int Uses { get; }

        public void GainUses();
        public void OnActivateBegin();
        public void OnActivateEnd();

    }

    public class Trampoline : ITool
    {
        public Trampoline()
        {
            Uses = InitialUses;
        }

        public int Uses { get; private set; }

        private int InitialUses = 5;

        public void GainUses()
        {
            Uses += InitialUses;
        }
        
        public void OnActivateBegin() 
        { 
            // Place Hologram of Trampoline at Cursor Raycast Position
        }

        public void OnActivateEnd() 
        { 
            // Transform Hologram into Trampoline
        }
    }
}
