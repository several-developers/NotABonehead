using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Battle.Entities
{
    public abstract class EntityBrain : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Stats)]
        [SerializeField, InlineProperty, HideLabel]
        protected EntityStats _entityStats;

        // FIELDS: --------------------------------------------------------------------------------
        
        private const string Stats = "Stats";

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int State = Animator.StringToHash("State");

        private IEntityTracker _entityTracker;
        private Animator _animator;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            _animator = GetComponent<Animator>();

        private void OnDestroy()
        {
            _entityTracker.OnAttackEvent -= OnAttackEvent;
            _entityTracker.OnTakeDamageEvent -= OnTakeDamageEvent;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Setup(IEntityTracker entityTracker, EntityStats playerStats)
        {
            _entityTracker = entityTracker;
            _entityStats = playerStats;

            _entityTracker.SetEntityBrain(this);

            _entityTracker.OnAttackEvent += OnAttackEvent;
            _entityTracker.OnTakeDamageEvent += OnTakeDamageEvent;
        }

        public EntityStats GetEntityStats() => _entityStats;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void PlayAttackAnimation() =>
            _animator.SetTrigger(Attack);

        private void PlayDeathAnimation() =>
            _animator.SetInteger(State, 9);

        private void TakeDamage(float damage)
        {
            float health = _entityStats.Health;
            health = Mathf.Max(health - damage, 0);

            _entityStats.SetHealth(health);
            _entityTracker.SendHealthChanged(currentHealth: health, _entityStats.MaxHealth);

            if (health > 0)
                return;

            Die();
        }

        private void Die()
        {
            PlayDeathAnimation();
            _entityTracker.SendDied();

            _entityTracker.OnTakeDamageEvent -= TakeDamage;
            _entityTracker.OnAttackEvent -= OnAttackEvent;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnAttackEvent() => PlayAttackAnimation();

        private void OnTakeDamageEvent(float damage) => TakeDamage(damage);

        // DEBUG BUTTONS: -------------------------------------------------------------------------

        [Title(Constants.DebugButtons)]
        [Button(25, ButtonStyle.FoldoutButton), DisableInEditorMode]
        private void DebugTakeDamage(float damage) => TakeDamage(damage);

        [Button(25), DisableInEditorMode]
        private void DebugDie() => Die();
    }
}