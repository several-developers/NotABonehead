using System;

namespace GameCore.Battle.Monsters
{
    public interface IMonsterTracker
    {
        event Action OnDoAttackEvent;
        event Action<MonsterStats> OnHealthChangedEvent;
        event Action OnDiedEvent;
        event Action<int> OnTakeDamageEvent;
        void SendHealthChanged(MonsterStats monsterStats);
        void SendDied();
        void SendAttack();
        void TakeDamage(int damage);
        void SetDamage(int damage);
        int GetDamage();
    }
}