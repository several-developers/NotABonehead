using System;
using GameCore.Enums;
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
            _health = health;
            _damage = damage;
            _defense = defense;
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private ItemRarity _rarity;
        
        [SerializeField, Min(1)]
        private int _level;
        
        [SerializeField, Min(0)]
        private int _health;
        
        [SerializeField, Min(0)]
        private int _damage;
        
        [SerializeField, Min(0)]
        private int _defense;

        // PROPERTIES: ----------------------------------------------------------------------------

        public ItemRarity Rarity => _rarity;
        public int Level => _level;
        public int Health => _health;
        public int Damage => _damage;
        public int Defense => _defense;
    }
}