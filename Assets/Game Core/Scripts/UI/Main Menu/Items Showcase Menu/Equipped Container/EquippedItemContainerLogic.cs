﻿using GameCore.Battle.Entities;
using GameCore.Configs;
using GameCore.Enums;
using GameCore.Factories;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using GameCore.Items;
using GameCore.UI.MainMenu.GameItems;
using UnityEngine;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    public class EquippedItemContainerLogic
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public EquippedItemContainerLogic(IInventoryService inventoryService, IConfigsProvider configsProvider,
            IItemsShowcaseService itemsShowcaseService, EquippedItemContainerVisualizer containerVisualizer,
            Transform itemContainer)
        {
            _inventoryService = inventoryService;
            _itemsShowcaseService = itemsShowcaseService;
            _itemsRarityConfig = configsProvider.GetItemsRarityConfig();
            _containerVisualizer = containerVisualizer;
            _itemContainer = itemContainer;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly IInventoryService _inventoryService;
        private readonly IItemsShowcaseService _itemsShowcaseService;
        private readonly ItemsRarityConfigMeta _itemsRarityConfig;
        private readonly EquippedItemContainerVisualizer _containerVisualizer;
        private readonly Transform _itemContainer;

        private GameItemView _gameItemView;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void UpdateContainerInfo()
        {
            TryDestroyGameItemView();
            
            bool isItemValid = IsItemValid(out ItemData itemData, out ItemMeta itemMeta);

            if (!isItemValid)
            {
                HideItemInfo();
                return;
            }

            CreateGameItemView(itemData);
            UpdateStatsItemInfo(itemData, itemMeta);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void HideItemInfo() =>
            _containerVisualizer.SetEquippedState(isEquipped: false);

        private void UpdateStatsItemInfo(ItemData itemData, ItemMeta itemMeta)
        {
            ItemStats itemStats = itemData.ItemStats;
            EntityStats entityStats = itemStats.EntityStats;
            ItemRarity itemRarity = itemStats.Rarity;
            
            ItemRarityConfig itemRarityConfig = _itemsRarityConfig.GetItemRarityConfig(itemRarity);
            Color rarityColor = itemRarityConfig.RarityColor;
            
            _containerVisualizer.SetItemName(itemRarity, itemMeta.ItemName);
            _containerVisualizer.SetItemNameColor(rarityColor);
            
            SetStatValue(StatType.Health, entityStats.Health);
            SetStatValue(StatType.Damage, entityStats.Damage);
            SetStatValue(StatType.Defense, entityStats.Defense);
            
            // LOCAL METHODS: -----------------------------

            void SetStatValue(StatType statType, float value) =>
                _containerVisualizer.SetStatValue(statType, value);
        }

        private void CreateGameItemView(ItemData itemData)
        {
            ItemViewParams itemViewParams = new(itemData.ItemID, useItemKey: false, isInteractable: false);

            ItemRarityParam itemRarityParam = new(itemData.ItemStats.Rarity);
            ItemLevelParam itemLevelParam = new(itemData.ItemStats.Level);
            
            itemViewParams.AddParam(itemRarityParam);
            itemViewParams.AddParam(itemLevelParam);

            GameItemsFactory.Create<WearableItemView, ItemViewParams>(_itemContainer, itemViewParams);
        }
        
        private bool IsItemValid(out ItemData itemData, out ItemMeta itemMeta)
        {
            itemData = null;
            itemMeta = null;
            
            ItemType itemType = _itemsShowcaseService.GetSelectedItemType();
            bool containsItem = IsItemEquipped(itemType, out string itemKey);

            if (!containsItem)
                return false;

            bool containsItemData = _inventoryService.TryGetItemData(itemKey, out itemData);

            if (!containsItemData)
                return false;

            bool containsItemMeta = _inventoryService.TryGetItemMetaByKey(itemKey, out itemMeta);

            if (!containsItemMeta)
                return false;

            return true;
        }
        
        private bool IsItemEquipped(ItemType itemType, out string itemKey) =>
            _inventoryService.TryGetEquippedItemKey(itemType, out itemKey);

        private void TryDestroyGameItemView()
        {
            if (_gameItemView == null)
                return;
            
            Object.Destroy(_gameItemView.gameObject);
            _gameItemView = null;
        }
    }
}