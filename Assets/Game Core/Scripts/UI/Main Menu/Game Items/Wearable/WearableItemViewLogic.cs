﻿using GameCore.Configs;
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

        public WearableItemViewLogic(IInventoryService inventoryService, IConfigsProvider configsProvider,
            WearableItemViewVisualizer itemVisualizer)
        {
            _itemsRarityConfig = configsProvider.GetItemsRarityConfig();
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

            bool isItemMetaValid = IsItemMetaValid(itemID, out ItemMeta itemMeta);

            if (!isItemMetaValid)
                return;
            
            var itemRarityParam = itemParams.GetParam<ItemRarityParam>();
            ItemRarity itemRarity = itemRarityParam.GetValue();

            var itemLevelParam = itemParams.GetParam<ItemLevelParam>();
            int itemLevel = itemLevelParam.GetValue();

            ItemRarityConfig itemRarityConfig = _itemsRarityConfig.GetItemRarityConfig(itemRarity);
            Sprite rarityFrame = itemRarityConfig.RarityFrame;

            _itemVisualizer.SetItemIcon(itemMeta.Icon);
            _itemVisualizer.SetFrameImage(rarityFrame);
            _itemVisualizer.SetItemLevel(itemLevel);
        }

        private bool IsItemMetaValid(string itemID, out ItemMeta result) =>
            _inventoryService.TryGetItemMetaByID(itemID, out result);
    }
}