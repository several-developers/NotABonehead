using Cysharp.Threading.Tasks;
using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using GameCore.MainMenu;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI.MainMenu.HUD.Character
{
    public class CharacterButtonView : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IItemsShowcaseService itemsShowcaseService, IRewardsService rewardsService,
            IPlayerPreviewObserver playerPreviewObserver)
        {
            _playerPreviewObserver = playerPreviewObserver;
            _characterButtonLogic = new CharacterButtonLogic(itemsShowcaseService, rewardsService);
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _clickDelay = 1f;

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _characterButton;
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private IPlayerPreviewObserver _playerPreviewObserver;
        private CharacterButtonLogic _characterButtonLogic;
        private bool _isBlocked;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            _characterButton.onClick.AddListener(OnCharacterClicked);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void HandleClick()
        {
            bool containsDroppedItem = _characterButtonLogic.ContainsDroppedItem();

            if (!containsDroppedItem)
            {
                _playerPreviewObserver.SendClickEvent();

                int delay = (int)(_clickDelay * 1000);
                bool isCanceled = await UniTask
                    .Delay(delay, cancellationToken: this.GetCancellationTokenOnDestroy())
                    .SuppressCancellationThrow();

                if (isCanceled)
                    return;
            }

            _isBlocked = false;
            _characterButtonLogic.HandleClickLogic();
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