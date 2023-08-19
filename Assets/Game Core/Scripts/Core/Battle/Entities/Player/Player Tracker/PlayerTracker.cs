using System;

namespace GameCore.Battle.Player
{
    public class PlayerTracker : IPlayerTracker
    {
        // FIELDS: --------------------------------------------------------------------------------

        public event Action OnDoAttackEvent;
        public event Action<PlayerStats> OnHealthChangedEvent;
        public event Action OnDiedEvent;
        public event Action<float> OnTakeDamageEvent;

        private float _damage;
        private float _defense;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SendHealthChanged(PlayerStats monsterStats) =>
            OnHealthChangedEvent?.Invoke(monsterStats);

        public void SendDied() =>
            OnDiedEvent?.Invoke();
        
        public void SendAttack() =>
            OnDoAttackEvent?.Invoke();
        
        public void TakeDamage(float damage) =>
            OnTakeDamageEvent?.Invoke(damage);
        
        public void SetDamage(float damage) =>
            _damage = damage;

        public void SetDefense(float defense) =>
            _defense = defense;

        public float GetDamage() => _damage;
        
        public float GetDefense() => _defense;
    }
}