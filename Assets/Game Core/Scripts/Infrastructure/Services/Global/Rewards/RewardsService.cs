using System;
using System.Collections.Generic;
using GameCore.Configs;
using GameCore.Enums;
using GameCore.Factories;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Providers.Global.ItemsMeta;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Items;
using GameCore.UI.MainMenu.GameItems;
using GameCore.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCore.Infrastructure.Services.Global.Rewards
{
    public class RewardsService : IRewardsService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public RewardsService(IInventoryService inventoryService, IItemsMetaProvider itemsMetaProvider,
            IAssetsProvider assetsProvider)
        {
            _inventoryService = inventoryService;
            _itemsMetaProvider = itemsMetaProvider;
            _assetsProvider = assetsProvider;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private const int MaxItemTypesRepeats = 3;
        private const int MinStatValue = 1;
        private const int MaxStatValue = 15;

        private readonly IInventoryService _inventoryService;
        private readonly IItemsMetaProvider _itemsMetaProvider;
        private readonly IAssetsProvider _assetsProvider;

        private ItemType _previousItemType;
        private int _sameItemTypeRepeats;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void GiveItemReward(string itemID, ItemStats itemStats, bool autoSave = true) =>
            _inventoryService.AddItem(itemID, itemStats, autoSave);

        public bool GiveItemReward(Transform container, ItemType itemType, ItemRarity itemRarity,
            out GameItemView gameItemView)
        {
            bool itemsTypeRepeats = CheckIfItemsTypeRepeats(itemType);
            gameItemView = null;

            if (itemsTypeRepeats)
                itemType = GetRandomItemType(itemType);

            _previousItemType = itemType;

            bool isItemMetaFound = TryGetRandomItemMeta(itemType, out ItemMeta itemMeta);

            if (!isItemMetaFound)
                return false;

            ItemStats itemStats = CreateItemStats(itemRarity);
            GiveItemReward(itemMeta.ItemID, itemStats);

            ItemViewParams itemViewParams = new(itemMeta.ItemID, useItemKey: false, isInteractable: true);

            ItemRarityParam itemRarityParam = new(itemStats.Rarity);
            ItemLevelParam itemLevelParam = new(itemStats.Level);

            itemViewParams.AddParam(itemRarityParam);
            itemViewParams.AddParam(itemLevelParam);

            gameItemView = GameItemsFactory.Create<WearableItemView, ItemViewParams>(container, itemViewParams);
            return true;
        }

        public bool GiveRandomItemReward(Transform container, out GameItemView gameItemView)
        {
            ItemType itemType = GetRandomItemType();
            ItemRarity itemRarity = GetRandomItemRarity();

            return GiveItemReward(container, itemType, itemRarity, out gameItemView);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private ItemType GetRandomItemType()
        {
            int typesAmount = Enum.GetNames(typeof(ItemType)).Length;
            int randomValue = Random.Range(0, typesAmount);
            ItemType itemType = (ItemType)randomValue;

            return itemType;
        }

        private ItemType GetRandomItemType(ItemType excludedItemType)
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

        private ItemRarity GetRandomItemRarity()
        {
            ItemsDropChancesConfigMeta itemsDropChancesConfig = _assetsProvider.GetItemsDropChancesConfigMeta();
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
            List<WearableItemMeta> itemsMetaWithCorrectType = new(capacity: 6);
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
                IEnumerable<WearableItemMeta> allItemsMeta = _itemsMetaProvider.GetAllWearableItemsMeta();

                foreach (WearableItemMeta wearableItemMeta in allItemsMeta)
                {
                    if (wearableItemMeta.ItemType != itemType)
                        continue;

                    itemsMetaWithCorrectType.Add(wearableItemMeta);
                }
            }
        }

        private ItemStats CreateItemStats(ItemRarity itemRarity = ItemRarity.Common)
        {
            int level = 1;
            int health = GetRandomStatValue();
            int damage = GetRandomStatValue();
            int defense = GetRandomStatValue();

            ItemStats itemStats = new(itemRarity, level, health, damage, defense);
            return itemStats;
        }

        private int GetRandomStatValue() =>
            Random.Range(MinStatValue, MaxStatValue + 1);
    }
}