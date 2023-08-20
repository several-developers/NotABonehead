using System;

namespace GameCore.Battle.Entities.Player
{
    public class PlayerTracker : IPlayerTracker
    {
        // FIELDS: --------------------------------------------------------------------------------

        public event Action<float> OnTakeDamageEvent;
        public event Action<float, float> OnHealthChangedEvent;
        public event Action OnAttackEvent;
        public event Action OnDiedEvent;

        private EntityBrain _entityBrain;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetEntityBrain(EntityBrain entityBrain) =>
            _entityBrain = entityBrain;

        public void TakeDamage(float damage) =>
            OnTakeDamageEvent?.Invoke(damage);

        public void SendHealthChanged(float currentHealth, float maxHealth) =>
            OnHealthChangedEvent?.Invoke(currentHealth, maxHealth);

        public void SendAttackEvent() =>
            OnAttackEvent?.Invoke();

        public void SendDied() =>
            OnDiedEvent?.Invoke();
        
        public EntityStats GetEntityStats() =>
            _entityBrain.GetEntityStats();
    }
}