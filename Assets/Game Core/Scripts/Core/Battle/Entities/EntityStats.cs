using System;
using UnityEngine;

namespace GameCore.Battle.Entities
{
    [Serializable]
    public struct EntityStats
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public EntityStats(float health, float damage, float defense)
        {
            _health = health;
            _damage = damage;
            _defense = defense;

            MaxHealth = health;
        }
        
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private float _health;

        [SerializeField]
        private float _damage;
        
        [SerializeField]
        private float _defense;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float Health => _health;
        public float Damage => _damage;
        public float Defense => _defense;
        
        public float MaxHealth { get; private set; }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetHealth(float health) =>
            _health = health;
        
        public void SetDamage(float damage) =>
            _damage = damage;
        
        public void SetDefense(float defense) =>
            _defense = defense;

        public void SetMaxHealth(float maxHealth) =>
            MaxHealth = maxHealth;
    }
}