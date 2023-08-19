using System;
using GameCore.Battle.Entities;
using GameCore.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Items
{
    [Serializable]
    public struct ItemStats
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemStats(ItemRarity rarity, int level, int health, int damage, int defense)
        {
            _rarity = rarity;
            _level = level;
            _entityStats = new EntityStats(health, damage, defense);
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private ItemRarity _rarity;
        
        [SerializeField, Min(1)]
        private int _level;

        [SerializeField, InlineProperty, HideLabel]
        private EntityStats _entityStats;

        // PROPERTIES: ----------------------------------------------------------------------------

        public ItemRarity Rarity => _rarity;
        public int Level => _level;
        public EntityStats EntityStats => _entityStats;
    }
}