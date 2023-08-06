using System.Collections.Generic;
using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Services.Global.Inventory;

namespace GameCore.UI.MainMenu.InventoryMenu.PlayerStatsPanel
{
    public class PlayerStatLogic
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public PlayerStatLogic(IInventoryService inventoryService, PlayerStatVisualizer statVisualizer, StatType statType)
        {
            _inventoryService = inventoryService;
            _statVisualizer = statVisualizer;
            _statType = statType;

            _inventoryService.OnItemEquippedEvent += OnItemEquippedEvent;
            _inventoryService.OnItemUnEquippedEvent += OnItemUnEquippedEvent;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly IInventoryService _inventoryService;
        private readonly PlayerStatVisualizer _statVisualizer;
        private readonly StatType _statType;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void UpdateStatInfo()
        {
            IEnumerable<string> allEquippedItemsKeys = _inventoryService.GetAllEquippedItemsKeys();
            int statValue = 0;

            foreach (string itemKey in allEquippedItemsKeys)
            {
                bool isItemDataFound = _inventoryService.TryGetItemData(itemKey, out ItemData itemData);
                
                if (!isItemDataFound)
                    continue;

                switch (_statType)
                {
                    case StatType.Health:
                        statValue += itemData.ItemStats.Health;
                        break;
                    
                    case StatType.Damage:
                        statValue += itemData.ItemStats.Damage;
                        break;
                    
                    case StatType.Defense:
                        statValue += itemData.ItemStats.Defense;
                        break;
                }
            }
            
            _statVisualizer.SetValue(statValue);
        }
        
        public void Dispose()
        {
            _inventoryService.OnItemEquippedEvent -= OnItemEquippedEvent;
            _inventoryService.OnItemUnEquippedEvent -= OnItemUnEquippedEvent;
        }
        
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnItemEquippedEvent() => UpdateStatInfo();

        private void OnItemUnEquippedEvent() => UpdateStatInfo();
    }
}