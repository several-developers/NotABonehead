using System;
using UnityEngine;

namespace GameCore.Items
{
    [Serializable]
    public struct ItemStats
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemStats(int damage, int defense, int health)
        {
            _damage = damage;
            _defense = defense;
            _health = health;
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Min(0)]
        private int _damage;
        
        [SerializeField, Min(0)]
        private int _defense;
        
        [SerializeField, Min(0)]
        private int _health;

        // PROPERTIES: ----------------------------------------------------------------------------

        public int Damage => _damage;
        public int Defense => _defense;
        public int Health => _health;
    }
}