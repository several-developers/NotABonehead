using System;

namespace GameCore.Battle.Entities
{
    public interface IEntityTracker
    {
        event Action<float> OnTakeDamageEvent;
        event Action<HealthDifferenceData> OnHealthChangedEvent;
        event Action OnAttackEvent;
        event Action OnDiedEvent;
        void SetEntityBrain(EntityBrain entityBrain);
        void TakeDamage(float damage);
        void SendHealthChanged(float currentHealth, float maxHealth);
        void SendAttackEvent();
        void SendDieEvent();
        EntityStats GetEntityStats();
    }
}