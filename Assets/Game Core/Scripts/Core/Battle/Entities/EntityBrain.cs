using GameCore.Battle.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Battle.Player
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

        private Animator _animator;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            _animator = GetComponent<Animator>();
        
        // PROTECTED METHODS: ---------------------------------------------------------------------
        
        protected void PlayAttackAnimation() =>
            _animator.SetTrigger(Attack);

        protected void PlayDeathAnimation() =>
            _animator.SetInteger(State, 9);
    }
}