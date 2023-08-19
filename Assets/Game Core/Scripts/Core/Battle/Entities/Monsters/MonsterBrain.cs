using GameCore.Battle.Entities;
using GameCore.Battle.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Battle.Monsters
{
    public class MonsterBrain : EntityBrain
    {
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private IMonsterTracker _monsterTracker;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy() =>
            _monsterTracker.OnTakeDamageEvent -= TakeDamage;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Setup(IMonsterTracker monsterTracker, EntityStats monsterStats)
        {
            _monsterTracker = monsterTracker;
            _entityStats = monsterStats;
            
            _monsterTracker.SetDamage(monsterStats.Damage);
            _monsterTracker.OnDoAttackEvent += PlayAttackAnimation;
            _monsterTracker.OnTakeDamageEvent += TakeDamage;
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        [Button]
        private void TakeDamage(float damage)
        {
            float health = _entityStats.Health;
            health = Mathf.Max(health - damage, 0);

            MonsterStats monsterStats = new(currentHealth: health, _entityStats.MaxHealth);
            _monsterTracker.SendHealthChanged(monsterStats);
            
            if (health <= 0)
                Die();
        }

        [Button]
        private void Die()
        {
            PlayDeathAnimation();
            _monsterTracker.SendDied();
            _monsterTracker.OnTakeDamageEvent -= TakeDamage;
        }
    }
}