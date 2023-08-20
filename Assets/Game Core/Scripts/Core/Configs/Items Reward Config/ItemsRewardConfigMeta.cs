using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Configs
{
    public class ItemsRewardConfigMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(ConfigSettings)]
        [SerializeField, Min(1)]
        private int _minStatValue = 1;

        [SerializeField, Min(1)]
        private int _maxStatValue = 15;

        [SerializeField, Min(1)]
        private int _minItemLevel = 1;

        [SerializeField, Min(1)]
        private int _maxItemLevel = 10;

        [SerializeField, Min(1)]
        private int _maxSameTypesRepeats = 3;

        // PROPERTIES: ----------------------------------------------------------------------------

        public int MinStatValue => _minStatValue;
        public int MaxStatValue => _maxStatValue;
        public int MinItemLevel => _minItemLevel;
        public int MaxItemLevel => _maxItemLevel;
        public int MaxSameTypesRepeats => _maxSameTypesRepeats;

        // FIELDS: --------------------------------------------------------------------------------

        private const string ConfigSettings = "Config Settings";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override string GetMetaCategory() =>
            EditorConstants.ConfigsCategory;
    }
}