using System;

namespace GameCore.Battle.Player
{
    public interface IPlayerTracker
    {
        event Action OnDoAttackEvent;
        event Action<PlayerStats> OnHealthChangedEvent;
        event Action OnDiedEvent;
        event Action<int> OnTakeDamageEvent;
        void SendHealthChanged(PlayerStats monsterStats);
        void SendDied();
        void SendAttack();
        void TakeDamage(int damage);
        void SetDamage(int damage);
        int GetDamage();
    }
}