using System;

namespace GameCore.Battle.Monsters
{
    public class MonsterTracker : IMonsterTracker
    {
        // FIELDS: --------------------------------------------------------------------------------

        public event Action OnDoAttackEvent;
        public event Action<MonsterStats> OnHealthChangedEvent;
        public event Action OnDiedEvent;
        public event Action<float> OnTakeDamageEvent;

        private float _damage;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SendHealthChanged(MonsterStats monsterStats) =>
            OnHealthChangedEvent?.Invoke(monsterStats);

        public void SendDied() =>
            OnDiedEvent?.Invoke();

        public void SendAttack() =>
            OnDoAttackEvent?.Invoke();

        public void TakeDamage(float damage) =>
            OnTakeDamageEvent?.Invoke(damage);

        public void SetDamage(float damage) =>
            _damage = damage;

        public float GetDamage() => _damage;
    }
}