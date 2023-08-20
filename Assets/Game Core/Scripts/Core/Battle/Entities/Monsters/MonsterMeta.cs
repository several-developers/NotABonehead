using GameCore.Battle.Entities;
using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Battle.Entities.Monsters
{
    public class MonsterMeta : EditorMeta
    {
        [TitleGroup(ItemSettings)]
        [HorizontalGroup(Row, 80), VerticalGroup(RowLeft)]
        [PreviewField(60, ObjectFieldAlignment.Left), SerializeField, HideLabel, AssetsOnly]
        private Sprite _icon;
        
        [VerticalGroup(RowRight), SerializeField, Min(1)]
        private int _health = 10;
        
        [VerticalGroup(RowRight), SerializeField, Min(1)]
        private int _damage = 3;

        [Title(StatsTitle)]
        [SerializeField, InlineProperty, HideLabel]
        private EntityStats _monsterStats;

        [VerticalGroup(RowRight), SerializeField, Required]
        private GameObject _monsterPrefab;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public Sprite Icon => _icon;
        public int Health => _health;
        public int Damage => _damage;
        public EntityStats MonsterStats => _monsterStats;
        public GameObject MonsterPrefab => _monsterPrefab;
        
        // FIELDS: --------------------------------------------------------------------------------
        
        private const string ItemSettings = "Wearable Item Settings";
        private const string Row = ItemSettings + "/Row";
        private const string RowLeft = Row + "/Left";
        private const string RowRight = Row + "/Right";
        private const string StatsTitle = "Monster Stats";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override string GetMetaCategory() =>
            EditorConstants.MonstersCategory;
    }
}