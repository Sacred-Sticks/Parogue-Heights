﻿namespace Parogue_Heights
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
                ThrusterSpell => SpellType.Thruster,
                SmasherSpell => SpellType.Rebound,
                _ => default,
            };
        }

        public static void LowerUses(ISpell spell)
        {
            spell.Uses--;
            if (spell.Uses <= 0)
                Inventory.Instance.RemoveSlot(spell.InventorySlot);
        }

        public enum SpellType
        {
            Hookshot,
            Jump,
            Thruster,
            Bounce,
            Rebound,
        }

        public int Uses { get; set; }
        public InventorySlot InventorySlot { get; set; }

        public void GainUses();
        public void OnActivateBegin();
        public void OnActivateEnd()
        {
            // Noop
        }
    }
}
