using GameCore.Battle.Entities;
using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Configs
{
    public class PlayerConfigMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(StatsTitle)]
        [SerializeField, InlineProperty, HideLabel]
        private EntityStats _playerStats;

        [Title(Constants.References)]
        [SerializeField, Required]
        private GameObject _playerPrefab;

        // PROPERTIES: ----------------------------------------------------------------------------

        public EntityStats PlayerStats => _playerStats;
        public GameObject PlayerPrefab => _playerPrefab;

        // FIELDS: --------------------------------------------------------------------------------
        
        private const string StatsTitle = "Player Stats";
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public override string GetMetaCategory() =>
            EditorConstants.ConfigsCategory;
    }
}