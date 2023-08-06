using GameCore.Configs;
using GameCore.Enums;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Items;
using UnityEngine;

namespace GameCore.UI.MainMenu.GameItems
{
    public class WearableItemViewLogic
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public WearableItemViewLogic(IInventoryService inventoryService, IAssetsProvider assetsProvider,
            WearableItemViewVisualizer itemVisualizer)
        {
            _itemsRarityConfig = assetsProvider.GetItemsRarityConfigMeta();
            _inventoryService = inventoryService;
            _itemVisualizer = itemVisualizer;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ItemsRarityConfigMeta _itemsRarityConfig;
        private readonly IInventoryService _inventoryService;
        private readonly WearableItemViewVisualizer _itemVisualizer;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void UpdateItemInfo(ItemViewParams itemParams)
        {
            if (!itemParams.UseItemKey)
                UpdateItemInfoByItemID(itemParams);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void UpdateItemInfoByItemID(ItemViewParams itemParams)
        {
            string itemID = itemParams.ItemKeyOrID;

            bool isItemMetaValid = IsItemMetaValid(itemID, out WearableItemMeta wearableItemMeta);

            if (!isItemMetaValid)
                return;
            
            var itemRarityParam = itemParams.GetParam<ItemRarityParam>();
            ItemRarity itemRarity = itemRarityParam.GetValue();

            var itemLevelParam = itemParams.GetParam<ItemLevelParam>();
            int itemLevel = itemLevelParam.GetValue();

            ItemRarityConfig itemRarityConfig = _itemsRarityConfig.GetItemRarityConfig(itemRarity);
            Sprite rarityFrame = itemRarityConfig.RarityFrame;

            _itemVisualizer.SetItemIcon(wearableItemMeta.Icon);
            _itemVisualizer.SetFrameImage(rarityFrame);
            _itemVisualizer.SetItemLevel(itemLevel);
        }

        private bool IsItemMetaValid(string itemID, out WearableItemMeta result)
        {
            bool containsItemMeta = _inventoryService.TryGetItemMetaByID(itemID, out ItemMeta itemMeta);
            result = null;

            if (!containsItemMeta)
                return false;

            bool isMetaCorrectType = itemMeta is WearableItemMeta;

            if (!isMetaCorrectType)
                return false;

            result = (WearableItemMeta)itemMeta;

            return true;
        }
    }
}