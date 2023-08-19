using System;

namespace GameCore.Battle.Monsters
{
    public interface IMonsterTracker
    {
        event Action OnDoAttackEvent;
        event Action<MonsterStats> OnHealthChangedEvent;
        event Action OnDiedEvent;
        event Action<float> OnTakeDamageEvent;
        void SendHealthChanged(MonsterStats monsterStats);
        void SendDied();
        void SendAttack();
        void TakeDamage(float damage);
        void SetDamage(float damage);
        float GetDamage();
    }
}