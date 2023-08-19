using Cysharp.Threading.Tasks;
using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using GameCore.Other;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI.MainMenu.Character
{
    public class CharacterView : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IItemsShowcaseService itemsShowcaseService, IRewardsService rewardsService) =>
            _characterLogic = new CharacterLogic(itemsShowcaseService, rewardsService);

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _clickDelay = 1f;

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _characterButton;

        [SerializeField, Required]
        private PlayerCharacter _playerCharacter;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private CharacterLogic _characterLogic;
        private bool _isBlocked;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            _characterButton.onClick.AddListener(OnCharacterClicked);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void HandleClick()
        {
            bool containsDroppedItem = _characterLogic.ContainsDroppedItem();

            if (!containsDroppedItem)
            {
                _playerCharacter.PlayRandomAnimation();

                int delay = (int)(_clickDelay * 1000);
                await UniTask.Delay(delay);
            }

            _isBlocked = false;
            _characterLogic.HandleClickLogic();
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