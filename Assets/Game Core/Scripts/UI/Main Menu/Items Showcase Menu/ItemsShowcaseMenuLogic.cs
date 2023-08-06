using GameCore.Enums;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    public class ItemsShowcaseMenuLogic
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemsShowcaseMenuLogic(IInventoryService inventoryService, IItemsShowcaseService itemsShowcaseService,
            IRewardsService rewardsService, ItemsShowcaseMenuVisualizer menuVisualizer)
        {
            _inventoryService = inventoryService;
            _itemsShowcaseService = itemsShowcaseService;
            _rewardsService = rewardsService;
            _menuVisualizer = menuVisualizer;
        }

        // FIELDS: --------------------------------------------------------------------------------
        
        private readonly IInventoryService _inventoryService;
        private readonly IItemsShowcaseService _itemsShowcaseService;
        private readonly IRewardsService _rewardsService;
        private readonly ItemsShowcaseMenuVisualizer _menuVisualizer;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void UpdateButtonsState()
        {
            ItemType itemType = _itemsShowcaseService.GetSelectedItemType();
            bool containsEquippedItem = _inventoryService.TryGetEquippedItemKey(itemType, out string _);
            
            _menuVisualizer.SetItemEquippedState(containsEquippedItem);
        }
        
        public void HandleEquipLogic() =>
            _inventoryService.EquipDroppedItem();

        public void HandleDropItemLogic() =>
            _rewardsService.TrySellDroppedItem();
    }
}