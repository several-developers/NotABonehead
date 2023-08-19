using System;
using System.Collections.Generic;
using GameCore.Battle.Entities;
using GameCore.Configs;
using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using UnityEngine;

namespace GameCore.Infrastructure.Services.Global.Player
{
    public class PlayerStatsCalculatorService : IPlayerStatsCalculatorService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public PlayerStatsCalculatorService(IInventoryService inventoryService, IConfigsProvider configsProvider)
        {
            _inventoryService = inventoryService;
            _playerConfig = configsProvider.GetPlayerConfig();
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly IInventoryService _inventoryService;
        private readonly PlayerConfigMeta _playerConfig;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public EntityStats GetPlayerStats() => CalculateStats();

        public float GetPlayerStatValue(StatType statType)
        {
            EntityStats playerStats = GetPlayerStats();

            switch (statType)
            {
                case StatType.Health: return playerStats.Health;
                case StatType.Damage: return playerStats.Damage;
                case StatType.Defense: return playerStats.Defense;
                default: return 0;
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private EntityStats CalculateStats()
        {
            EntityStats playerStats = _playerConfig.PlayerStats;
            EntityStats itemsStats = GetItemsStats();

            float finalHealth = Mathf.Max(playerStats.Health + itemsStats.Health, 0);
            float finalDamage = Mathf.Max(playerStats.Damage + itemsStats.Damage, 0);
            float finalDefense = Mathf.Max(playerStats.Defense + itemsStats.Defense, 0);

            EntityStats newPlayerStats = new(finalHealth, finalDamage, finalDefense);
            return newPlayerStats;
        }
        
        private EntityStats GetItemsStats()
        {
            IEnumerable<string> allEquippedItemsKeys = _inventoryService.GetAllEquippedItemsKeys();
            
            EntityStats itemsStats = new();
            int statsAmount = Enum.GetNames(typeof(StatType)).Length;

            foreach (string itemKey in allEquippedItemsKeys)
            {
                if (!IsItemDataFound(itemKey, out ItemData itemData))
                    continue;

                GetStats(itemData, statsAmount, ref itemsStats);
            }

            return itemsStats;
            
            // LOCAL METHODS: -----------------------------

            bool IsItemDataFound(string itemKey, out ItemData result) =>
                _inventoryService.TryGetItemData(itemKey, out result);
        }
        
        private static void GetStats(ItemData itemData, int statsAmount, ref EntityStats itemsStats)
        {
            EntityStats itemDataStats = itemData.ItemStats.EntityStats;
            
            for (int i = 0; i < statsAmount; i++)
            {
                var statType = (StatType)i;
                float statValue;

                switch (statType)
                {
                    case StatType.Health:
                        statValue = itemsStats.Health + itemDataStats.Health;
                        itemsStats.SetHealth(statValue);
                        break;

                    case StatType.Damage:
                        statValue = itemsStats.Damage + itemDataStats.Damage;
                        itemsStats.SetDamage(statValue);
                        break;

                    case StatType.Defense:
                        statValue = itemsStats.Defense + itemDataStats.Defense;
                        itemsStats.SetDefense(statValue);
                        break;
                }
            }
        }
    }
}