using System;

namespace GameCore.Battle.Player
{
    public interface IPlayerTracker
    {
        event Action OnDoAttackEvent;
        event Action<PlayerStats> OnHealthChangedEvent;
        event Action OnDiedEvent;
        event Action<float> OnTakeDamageEvent;
        void SendHealthChanged(PlayerStats monsterStats);
        void SendDied();
        void SendAttack();
        void TakeDamage(float damage);
        void SetDamage(float damage);
        void SetDefense(float defense);
        float GetDamage();
        float GetDefense();
    }
}