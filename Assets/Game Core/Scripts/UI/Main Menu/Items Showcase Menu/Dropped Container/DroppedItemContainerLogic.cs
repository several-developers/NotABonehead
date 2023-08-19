using System;
using GameCore.Battle.Entities;
using GameCore.Configs;
using GameCore.Enums;
using GameCore.Factories;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Items;
using GameCore.UI.MainMenu.GameItems;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    public class DroppedItemContainerLogic
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public DroppedItemContainerLogic(IInventoryService inventoryService, IConfigsProvider configsProvider,
            EquippedItemContainerVisualizer containerVisualizer, Transform itemContainer)
        {
            _inventoryService = inventoryService;
            _itemsRarityConfig = configsProvider.GetItemsRarityConfig();
            _containerVisualizer = containerVisualizer;
            _itemContainer = itemContainer;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly IInventoryService _inventoryService;
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
            UpdateStatsArrowsInfo(itemData, itemMeta);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void HideItemInfo() =>
            _containerVisualizer.SetEquippedState(isEquipped: false);

        private void UpdateStatsItemInfo(ItemData itemData, ItemMeta itemMeta)
        {
            ItemStats itemStats = itemData.ItemStats;
            EntityStats entityStats = itemData.ItemStats.EntityStats;
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

        private void UpdateStatsArrowsInfo(ItemData itemData, ItemMeta itemMeta)
        {
            ItemType itemType = itemMeta.ItemType;
            bool isItemEquipped = IsItemEquipped(itemType, out string itemKey);

            if (!isItemEquipped)
                return;

            _inventoryService.TryGetItemData(itemKey, out ItemData equippedItemData);
            
            EntityStats equippedItemStats = equippedItemData.ItemStats.EntityStats;
            EntityStats droppedItemStats = itemData.ItemStats.EntityStats;

            SetArrowState(StatType.Health, droppedItemStats.Health, equippedItemStats.Health);
            SetArrowState(StatType.Damage, droppedItemStats.Damage, equippedItemStats.Damage);
            SetArrowState(StatType.Defense, droppedItemStats.Defense, equippedItemStats.Defense);

            SetDifference(StatType.Health, droppedItemStats.Health, equippedItemStats.Health);
            SetDifference(StatType.Damage, droppedItemStats.Damage, equippedItemStats.Damage);
            SetDifference(StatType.Defense, droppedItemStats.Defense, equippedItemStats.Defense);
            
            // LOCAL METHODS: -----------------------------

            void SetArrowState(StatType statType, float valueOne, float valueTwo)
            {
                ArrowState arrowState = GetStatArrowState(valueOne, valueTwo);
                _containerVisualizer.SetArrowState(statType, arrowState);
            }
            
            void SetDifference(StatType statType, float valueOne, float valueTwo)
            {
                float difference = valueOne - valueTwo;
                _containerVisualizer.SetDifference(statType, difference);
            }
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

            bool containsDroppedItem = _inventoryService.TryGetDroppedItemData(out itemData);

            if (!containsDroppedItem)
                return false;

            bool containsItemMeta = _inventoryService.TryGetItemMetaByID(itemData.ItemID, out itemMeta);

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

        private static ArrowState GetStatArrowState(float a, float b)
        {
            if (Math.Abs(a - b) < 0.0001f)
                return ArrowState.Hidden;

            if (a > b)
                return ArrowState.Up;

            return ArrowState.Down;
        }
    }
}