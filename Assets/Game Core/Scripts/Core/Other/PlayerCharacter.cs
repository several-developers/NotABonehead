using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Other
{
    public class PlayerCharacter : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _resetDelay = 0.9f;
        
        // FIELDS: --------------------------------------------------------------------------------

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int State = Animator.StringToHash("State");
        
        private Animator _animator;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public async void PlayRandomAnimation()
        {
            int randomValue = Random.Range(0, 3);

            switch (randomValue)
            {
                case 0:
                    _animator.SetTrigger(Attack);
                    break;
                
                case 1:
                    _animator.SetInteger(State, 2);
                    break;
                
                case 2:
                    _animator.SetInteger(State, 2);
                    break;
            }
            
            int delay = (int)(_resetDelay * 1000);
            await UniTask.Delay(delay);
            
            _animator.SetInteger(State, 0);
        }
    }
}