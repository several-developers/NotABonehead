using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Configs
{
    public class GameConfigMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(GameConfigSettings)]
        [SerializeField, Min(0)]
        private float _itemRewardDelay = 1f;

        [SerializeField, Min(0)]
        private float _droppedItemCreateDelay = 0.5f;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float ItemRewardDelay => _itemRewardDelay;
        public float DroppedItemCreateDelay => _droppedItemCreateDelay;
        
        // FIELDS: --------------------------------------------------------------------------------
        
        private const string GameConfigSettings = "Game Config Settings";
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override string GetMetaCategory() =>
            EditorConstants.ConfigsCategory;
    }
}