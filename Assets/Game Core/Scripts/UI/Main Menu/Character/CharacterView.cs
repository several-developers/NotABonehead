using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
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

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _characterButton;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private CharacterLogic _characterLogic;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Awake() =>
            _characterButton.onClick.AddListener(OnCharacterClicked);

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnCharacterClicked() =>
            _characterLogic.HandleClickLogic();
    }
}
