using System;

namespace Parogue_Heights
{
    public static class ToolFactory
    {
        public static Type[] _allTypes = new Type[]
        {
            typeof(Trampoline),
        };

        public static ITool CreateRandomTool()
        {
            int index = UnityEngine.Random.Range(0, _allTypes.Length);
            return _allTypes[index].GetConstructor(Type.EmptyTypes).Invoke(null) as ITool;
        }
    }
}
