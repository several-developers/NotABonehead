using System;
using System.Collections.Generic;
using GameCore.Battle.Entities;
using GameCore.Configs;
using GameCore.Enums;
using GameCore.Events;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Providers.Global.ItemsMeta;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Items;
using GameCore.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCore.Infrastructure.Services.Global.Rewards
{
    public class RewardsService : IRewardsService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public RewardsService(IInventoryService inventoryService, IItemsMetaProvider itemsMetaProvider,
            IConfigsProvider configsProvider, IPlayerDataService playerDataService)
        {
            _inventoryService = inventoryService;
            _itemsMetaProvider = itemsMetaProvider;
            _configsProvider = configsProvider;
            _playerDataService = playerDataService;
        }

        // FIELDS: --------------------------------------------------------------------------------
        
        private const int MaxItemTypesRepeats = 3;
        private const int MinStatValue = 1;
        private const int MaxStatValue = 15;
        private const int MinItemLevel = 1;
        private const int MaxItemLevel = 10;

        private readonly IInventoryService _inventoryService;
        private readonly IItemsMetaProvider _itemsMetaProvider;
        private readonly IConfigsProvider _configsProvider;
        private readonly IPlayerDataService _playerDataService;

        private ItemType _previousItemType;
        private int _sameItemTypeRepeats;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void GiveRandomItem(bool autoSave = true)
        {
            ItemType itemType = GetRandomItemType();
            ItemRarity itemRarity = GetRandomItemRarity();

            GiveItem(itemType, itemRarity, autoSave);
        }

        public void GiveItem(ItemType itemType, ItemRarity itemRarity, bool autoSave = true)
        {
            bool itemsTypeRepeats = CheckIfItemsTypeRepeats(itemType);

            if (itemsTypeRepeats)
                itemType = GetRandomItemType(itemType);

            _previousItemType = itemType;

            bool isItemMetaFound = TryGetRandomItemMeta(itemType, out ItemMeta itemMeta);

            if (!isItemMetaFound)
                return;

            ItemStats itemStats = CreateItemStats(itemRarity);
            GiveItem(itemMeta.ItemID, itemStats, autoSave);
        }

        public void GiveItem(string itemID, ItemStats itemStats, bool autoSave = true) =>
            _inventoryService.SetDroppedItemData(itemID, itemStats, autoSave);

        public void TrySellDroppedItem()
        {
            bool containsDroppedItem = _inventoryService.TryGetDroppedItemData(out ItemData itemData);

            if (!containsDroppedItem)
                return;
            
            _inventoryService.RemoveDroppedItemData(autoSave: false);

            EntityStats itemStats = itemData.ItemStats.EntityStats;
            int goldReward = CalculateItemGoldReward(itemStats);
            _playerDataService.AddGold(goldReward);

            GlobalEvents.SendCurrencyChanged();
        }

        public static int CalculateItemGoldReward(EntityStats itemStats)
        {
            float reward = 0;

            reward += itemStats.Health;
            reward += itemStats.Damage;
            reward += itemStats.Defense;

            return Mathf.RoundToInt(reward);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private ItemRarity GetRandomItemRarity()
        {
            ItemsDropChancesConfigMeta itemsDropChancesConfig = _configsProvider.GetItemsDropChancesConfig();
            int itemRaritiesAmount = Enum.GetNames(typeof(ItemRarity)).Length;
            double[] chances = new double[itemRaritiesAmount];

            GetDropChances();
            int randomIndex = GlobalUtilities.GetRandomIndex(chances);
            ItemRarity result = (ItemRarity)randomIndex;
            return result;

            // METHODS: -----------------------------------

            void GetDropChances()
            {
                Array itemRarities = Enum.GetValues(typeof(ItemRarity));

                foreach (ItemRarity rarity in itemRarities)
                {
                    float dropChance = itemsDropChancesConfig.GetItemDropChance(rarity);
                    double chance = dropChance;
                    int rarityIndex = (int)rarity;
                    chances[rarityIndex] = chance;
                }
            }
        }

        private bool CheckIfItemsTypeRepeats(ItemType itemType)
        {
            bool isSameType = itemType == _previousItemType;

            if (!isSameType)
            {
                _sameItemTypeRepeats = 1;
                return false;
            }

            bool isRepeatsLimitReached = _sameItemTypeRepeats >= MaxItemTypesRepeats;
            _sameItemTypeRepeats++;

            if (isRepeatsLimitReached)
                _sameItemTypeRepeats = 0;

            return isRepeatsLimitReached;
        }

        private bool TryGetRandomItemMeta(ItemType itemType, out ItemMeta result)
        {
            result = null;
            List<ItemMeta> itemsMetaWithCorrectType = new(capacity: 6);
            GetCorrectItemsMeta();

            int itemsAmount = itemsMetaWithCorrectType.Count;

            if (itemsAmount == 0)
                return false;

            int randomIndex = Random.Range(0, itemsAmount);
            result = itemsMetaWithCorrectType[randomIndex];
            return true;

            // METHODS: -----------------------------------

            void GetCorrectItemsMeta()
            {
                IEnumerable<ItemMeta> allItemsMeta = _itemsMetaProvider.GetAllItemsMeta();

                foreach (ItemMeta itemMeta in allItemsMeta)
                {
                    if (itemMeta.ItemType != itemType)
                        continue;

                    itemsMetaWithCorrectType.Add(itemMeta);
                }
            }
        }

        private static ItemStats CreateItemStats(ItemRarity itemRarity = ItemRarity.Common)
        {
            int level = GetRandomItemLevel();
            int health = GetRandomStatValue();
            int damage = GetRandomStatValue();
            int defense = GetRandomStatValue();

            ItemStats itemStats = new(itemRarity, level, health, damage, defense);
            return itemStats;
        }

        private static ItemType GetRandomItemType()
        {
            int typesAmount = Enum.GetNames(typeof(ItemType)).Length;
            int randomValue = Random.Range(0, typesAmount);
            ItemType itemType = (ItemType)randomValue;

            return itemType;
        }

        private static ItemType GetRandomItemType(ItemType excludedItemType)
        {
            int typesAmount = Enum.GetNames(typeof(ItemType)).Length;
            ItemType itemType;

            const int maxIterations = 100;
            int iterations = 0;

            while (true)
            {
                int randomValue = Random.Range(0, typesAmount);
                itemType = (ItemType)randomValue;

                if (itemType != excludedItemType)
                    break;

                if (iterations >= maxIterations)
                    break;

                iterations++;
            }

            return itemType;
        }

        private static int GetRandomStatValue() =>
            Random.Range(MinStatValue, MaxStatValue + 1);
        
        private static int GetRandomItemLevel() =>
            Random.Range(MinItemLevel, MaxItemLevel + 1);
    }
}