using Kickstarter.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Parogue_Heights
{
    [CreateAssetMenu(fileName = "New Spells", menuName = "Parogue Heights/Spells")]
    public class Spells : ScriptableObject
    {
        [SerializeField, EnumData(typeof(ISpell.SpellType))] private SpellData[] spells;

        private Dictionary<ISpell.SpellType, SpellData> SpellData { get; } = new Dictionary<ISpell.SpellType, SpellData>();

        private void OnEnable()
        {
            SpellData.LoadDictionary(spells);
        }

        public SpellData Get(ISpell.SpellType spellType)
        {
            return SpellData[spellType];
        }

        public SpellData[] All()
        {
            return spells.ToArray();
        }
    }
}
