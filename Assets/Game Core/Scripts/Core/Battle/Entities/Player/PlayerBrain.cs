using GameCore.Battle.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Battle.Player
{
    public class PlayerBrain : EntityBrain
    {
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private IPlayerTracker _playerTracker;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy() =>
            _playerTracker.OnTakeDamageEvent -= TakeDamage;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Setup(IPlayerTracker monsterTracker, EntityStats playerStats)
        {
            _playerTracker = monsterTracker;
            _entityStats = playerStats;

            _playerTracker.SetDamage(playerStats.Damage);
            _playerTracker.SetDefense(playerStats.Defense);
            _playerTracker.OnDoAttackEvent += PlayAttackAnimation;
            _playerTracker.OnTakeDamageEvent += TakeDamage;
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        
        [Button]
        private void TakeDamage(float damage)
        {
            float health = _entityStats.Health;
            health = Mathf.Max(health - damage, 0);

            PlayerStats playerStats = new(currentHealth: health, _entityStats.MaxHealth);
            _playerTracker.SendHealthChanged(playerStats);
            
            if (health <= 0)
                Die();
        }

        [Button]
        private void Die()
        {
            PlayDeathAnimation();
            _playerTracker.SendDied();
            _playerTracker.OnTakeDamageEvent -= TakeDamage;
        }
    }
}