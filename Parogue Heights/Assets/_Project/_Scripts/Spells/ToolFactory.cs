using System;
using UnityEngine;

namespace Parogue_Heights
{
    public static class ToolFactory
    {
        public static Type[] _allTypes = new Type[]
        {
            typeof(BounceSpell),
            typeof(HookshotSpell),
            typeof(JumpSpell),
            typeof(DashSpell),
            typeof(DashbackSpell),
        };

        public static ISpell CreateRandomTool()
        {
            int index = UnityEngine.Random.Range(0, _allTypes.Length);
            return _allTypes[index].GetConstructor(Type.EmptyTypes).Invoke(null) as ISpell;
        }
    }
}
