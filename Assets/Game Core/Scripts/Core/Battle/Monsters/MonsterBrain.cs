using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Battle.Monsters
{
    public class MonsterBrain : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("Stats")]
        [SerializeField, Min(0)]
        private int _health;
        
        [SerializeField, Min(0)]
        private int _damage;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int State = Animator.StringToHash("State");
        
        private IMonsterTracker _monsterTracker;
        private Animator _animator;
        private int _maxHealth;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy() =>
            _monsterTracker.OnTakeDamageEvent -= TakeDamage;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Setup(IMonsterTracker monsterTracker, int health, int damage)
        {
            _monsterTracker = monsterTracker;
            _health = Mathf.Max(health, 0);
            _damage = Mathf.Max(damage, 0);
            _maxHealth = health;

            _animator = GetComponent<Animator>();
            
            _monsterTracker.SetDamage(damage);
            _monsterTracker.OnDoAttackEvent += PlayAttackAnimation;
            _monsterTracker.OnTakeDamageEvent += TakeDamage;
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void PlayAttackAnimation() =>
            _animator.SetTrigger(Attack);

        [Button]
        private void TakeDamage(int damage)
        {
            _health = Mathf.Max(_health - damage, 0);

            MonsterStats monsterStats = new(currentHealth: _health, _maxHealth);
            _monsterTracker.SendHealthChanged(monsterStats);
            
            if (_health <= 0)
                Die();
        }

        [Button]
        private void Die()
        {
            _animator.SetInteger(State, 9);
            _monsterTracker.SendDied();
            _monsterTracker.OnTakeDamageEvent -= TakeDamage;
        }
    }
}