using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Configs
{
    public class MonstersConfigMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Title)]
        [SerializeField, Min(0), SuffixLabel("%", overlay: true)]
        private float _monstersStatsIncreasePerLevel = 0.2f;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float MonstersStatsIncreasePerLevel => _monstersStatsIncreasePerLevel;

        // FIELDS: --------------------------------------------------------------------------------
        
        private const string Title = "Monsters Config";
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public override string GetMetaCategory() =>
            EditorConstants.ConfigsCategory;
    }
}