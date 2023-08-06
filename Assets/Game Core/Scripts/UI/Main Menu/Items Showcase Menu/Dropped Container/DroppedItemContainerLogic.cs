using GameCore.Configs;
using GameCore.Enums;
using GameCore.Factories;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Items;
using GameCore.UI.MainMenu.GameItems;
using UnityEngine;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    public class DroppedItemContainerLogic
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------
        
        public DroppedItemContainerLogic(IInventoryService inventoryService, IAssetsProvider assetsProvider,
            EquippedItemContainerVisualizer containerVisualizer, Transform itemContainer)
        {
            _inventoryService = inventoryService;
            _itemsRarityConfig = assetsProvider.GetItemsRarityConfigMeta();
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
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void HideItemInfo() =>
            _containerVisualizer.SetEquippedState(isEquipped: false);
        
        private void UpdateStatsItemInfo(ItemData itemData, ItemMeta itemMeta)
        {
            ItemStats itemStats = itemData.ItemStats;
            ItemRarity itemRarity = itemStats.Rarity;
            
            ItemRarityConfig itemRarityConfig = _itemsRarityConfig.GetItemRarityConfig(itemRarity);
            Color rarityColor = itemRarityConfig.RarityColor;
            
            _containerVisualizer.SetItemName(itemRarity, itemMeta.ItemName);
            _containerVisualizer.SetItemNameColor(rarityColor);
            _containerVisualizer.SetStatValue(StatType.Health, itemStats.Health);
            _containerVisualizer.SetStatValue(StatType.Damage, itemStats.Damage);
            _containerVisualizer.SetStatValue(StatType.Defense, itemStats.Defense);
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
    }
}