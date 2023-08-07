using System;

namespace GameCore.Battle.Player
{
    public class PlayerTracker : IPlayerTracker
    {
        // FIELDS: --------------------------------------------------------------------------------

        public event Action OnDoAttackEvent;
        public event Action<PlayerStats> OnHealthChangedEvent;
        public event Action OnDiedEvent;
        public event Action<int> OnTakeDamageEvent;

        private int _damage;
        private int _defense;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SendHealthChanged(PlayerStats monsterStats) =>
            OnHealthChangedEvent?.Invoke(monsterStats);

        public void SendDied() =>
            OnDiedEvent?.Invoke();
        
        public void SendAttack() =>
            OnDoAttackEvent?.Invoke();
        
        public void TakeDamage(int damage) =>
            OnTakeDamageEvent?.Invoke(damage);
        
        public void SetDamage(int damage) =>
            _damage = damage;

        public void SetDefense(int defense) =>
            _defense = defense;

        public int GetDamage() => _damage;
        
        public int GetDefense() => _defense;
    }
}