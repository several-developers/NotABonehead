using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.MainMenu
{
    public class PlayerPreview : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerPreviewObserver playerPreviewObserver)
        {
            _playerPreviewObserver = playerPreviewObserver;
            
            _playerPreviewObserver.OnClickedEvent += OnClickedEvent;
        }
        
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _resetDelay = 0.9f;

        [TitleGroup(Constants.Animation)]
        [BoxGroup(Constants.AnimationIn, showLabel: false), SerializeField]
        private PlayerPreviewAnimation _previewAnimation;
        
        // FIELDS: --------------------------------------------------------------------------------
        
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int State = Animator.StringToHash("State");
        
        private IPlayerPreviewObserver _playerPreviewObserver;
        private Animator _animator;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Awake() =>
            _animator = GetComponent<Animator>();

        private void OnDestroy() =>
            _playerPreviewObserver.OnClickedEvent -= OnClickedEvent;
        
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private async void PlayRandomAnimation()
        {
            _previewAnimation.StartAnimation();
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
                    _animator.SetInteger(State, 9);
                    break;
            }
            
            int delay = (int)(_resetDelay * 1000);
            bool isCanceled = await UniTask
                .Delay(delay, cancellationToken: this.GetCancellationTokenOnDestroy())
                .SuppressCancellationThrow();

            if (isCanceled)
                return;
            
            _animator.SetInteger(State, 0);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnClickedEvent() => PlayRandomAnimation();
    }
}