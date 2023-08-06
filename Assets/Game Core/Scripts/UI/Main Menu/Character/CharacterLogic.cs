using GameCore.Factories;
using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using GameCore.UI.MainMenu.ItemsShowcaseMenu;

namespace GameCore.UI.MainMenu.Character
{
    public class CharacterLogic
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public CharacterLogic(IItemsShowcaseService itemsShowcaseService, IRewardsService rewardsService)
        {
            _itemsShowcaseService = itemsShowcaseService;
            _rewardsService = rewardsService;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly IItemsShowcaseService _itemsShowcaseService;
        private readonly IRewardsService _rewardsService;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void HandleClickLogic()
        {
            bool containsDroppedItem = ContainsDroppedItem();

            if (!containsDroppedItem)
                GiveRandomItem();
            
            CreateItemsShowcaseMenu();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private bool ContainsDroppedItem() =>
            _itemsShowcaseService.ContainsDroppedItem();

        private void GiveRandomItem() =>
            _rewardsService.GiveRandomItem();

        private static void CreateItemsShowcaseMenu() =>
            MenuFactory.Create<ItemsShowcaseMenuView>();
    }
}