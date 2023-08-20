using Cysharp.Threading.Tasks;
using GameCore.Configs;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using GameCore.MainMenu;
using GameCore.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI.MainMenu.HUD.Character
{
    public class PlayerButtonView : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IItemsShowcaseService itemsShowcaseService, IRewardsService rewardsService,
            IPlayerPreviewObserver playerPreviewObserver, IConfigsProvider configsProvider)
        {
            _playerPreviewObserver = playerPreviewObserver;
            _gameConfigMeta = configsProvider.GetGameConfig();
            _buttonLogic = new PlayerButtonLogic(itemsShowcaseService, rewardsService);
        }

        // MEMBERS: -------------------------------------------------------------------------------
        
        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _characterButton;
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private IPlayerPreviewObserver _playerPreviewObserver;
        private GameConfigMeta _gameConfigMeta;
        private PlayerButtonLogic _buttonLogic;
        private bool _isBlocked;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            _characterButton.onClick.AddListener(OnCharacterClicked);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void HandleClick()
        {
            bool containsDroppedItem = _buttonLogic.ContainsDroppedItem();

            if (!containsDroppedItem)
            {
                _playerPreviewObserver.SendClickEvent();

                int delay = _gameConfigMeta.ItemRewardDelay.ConvertToMilliseconds();
                bool isCanceled = await UniTask
                    .Delay(delay, cancellationToken: this.GetCancellationTokenOnDestroy())
                    .SuppressCancellationThrow();

                if (isCanceled)
                    return;
            }

            _isBlocked = false;
            _buttonLogic.HandleClickLogic();
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnCharacterClicked()
        {
            if (_isBlocked)
                return;

            _isBlocked = true;
            HandleClick();
        }
    }
}