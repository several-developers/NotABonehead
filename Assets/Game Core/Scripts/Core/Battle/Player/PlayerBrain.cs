using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Battle.Player
{
    public class PlayerBrain : MonoBehaviour
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
        
        private IPlayerTracker _playerTracker;
        private Animator _animator;
        private int _maxHealth;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy() =>
            _playerTracker.OnTakeDamageEvent -= TakeDamage;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Setup(IPlayerTracker monsterTracker, int health, int damage)
        {
            _playerTracker = monsterTracker;
            _health = Mathf.Max(health, 0);
            _damage = Mathf.Max(damage, 0);
            _maxHealth = health;

            _animator = GetComponent<Animator>();

            _playerTracker.SetDamage(_damage);
            _playerTracker.OnDoAttackEvent += PlayAttackAnimation;
            _playerTracker.OnTakeDamageEvent += TakeDamage;
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void PlayAttackAnimation() =>
            _animator.SetTrigger(Attack);
        
        [Button]
        private void TakeDamage(int damage)
        {
            _health = Mathf.Max(_health - damage, 0);

            PlayerStats playerStats = new(currentHealth: _health, _maxHealth);
            _playerTracker.SendHealthChanged(playerStats);
            
            if (_health <= 0)
                Die();
        }

        [Button]
        private void Die()
        {
            _animator.SetInteger(State, 9);
            _playerTracker.SendDied();
            _playerTracker.OnTakeDamageEvent -= TakeDamage;
        }
    }
}