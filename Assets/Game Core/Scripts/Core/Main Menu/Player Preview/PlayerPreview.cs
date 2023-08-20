using Cysharp.Threading.Tasks;
using GameCore.Configs;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.MainMenu
{
    public class PlayerPreview : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerPreviewObserver playerPreviewObserver, IConfigsProvider configsProvider)
        {
            _playerPreviewObserver = playerPreviewObserver;
            _gameConfig = configsProvider.GetGameConfig();
            
            _playerPreviewObserver.OnClickedEvent += OnClickedEvent;
        }
        
        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup(Constants.Animation)]
        [BoxGroup(Constants.AnimationIn, showLabel: false), SerializeField]
        private PlayerPreviewAnimation _previewAnimation;
        
        // FIELDS: --------------------------------------------------------------------------------
        
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int State = Animator.StringToHash("State");
        
        private IPlayerPreviewObserver _playerPreviewObserver;
        private GameConfigMeta _gameConfig;
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
            PlayAnimatorAnimation();
            
            int delay = _gameConfig.ItemRewardDelay.ConvertToMilliseconds();
            bool isCanceled = await UniTask
                .Delay(delay, cancellationToken: this.GetCancellationTokenOnDestroy())
                .SuppressCancellationThrow();

            if (isCanceled)
                return;
            
            ResetAnimatorAnimation();
        }

        private void PlayAnimatorAnimation()
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
                    _animator.SetInteger(State, 9);
                    break;
            }
        }

        private void ResetAnimatorAnimation() =>
            _animator.SetInteger(State, 0);

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnClickedEvent() => PlayRandomAnimation();
    }
}