using System.Collections;
using System.Collections.Generic;
using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Services.Global.Inventory;
using UnityEngine;

namespace GameCore.UI.MainMenu.InventoryMenu.PlayerStatsPanel
{
    public class PlayerStatLogic
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public PlayerStatLogic(IInventoryService inventoryService, PlayerStatVisualizer statVisualizer,
            MonoBehaviour coroutineRunner, StatType statType)
        {
            _inventoryService = inventoryService;
            _statVisualizer = statVisualizer;
            _coroutineRunner = coroutineRunner;
            _statType = statType;

            _inventoryService.OnItemEquippedEvent += OnItemEquippedEvent;
            _inventoryService.OnItemUnEquippedEvent += OnItemUnEquippedEvent;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private const float TextTypeDuration = 0.5f;
        
        private readonly IInventoryService _inventoryService;
        private readonly PlayerStatVisualizer _statVisualizer;
        private readonly MonoBehaviour _coroutineRunner;
        private readonly StatType _statType;

        private Coroutine _textTypeCO;
        private float _lastStatValue;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void UpdateStatInfo(bool instant = false)
        {
            int statValue = GetStatValue();

            if (instant)
                _statVisualizer.SetValue(statValue);
            else
                UpdateStatWithAnimation(statValue);
            
            _lastStatValue = statValue;
        }
        
        public void Dispose()
        {
            StopTextTypeCO();

            _inventoryService.OnItemEquippedEvent -= OnItemEquippedEvent;
            _inventoryService.OnItemUnEquippedEvent -= OnItemUnEquippedEvent;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private int GetStatValue()
        {
            IEnumerable<string> allEquippedItemsKeys = _inventoryService.GetAllEquippedItemsKeys();
            int statValue = 0;

            switch (_statType)
            {
                case StatType.Health:
                    statValue += 50;
                    break;
                
                case StatType.Damage:
                    statValue += 5;
                    break;
            }

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

            return statValue;
        }

        private void UpdateStatWithAnimation(int statValue) =>
            _textTypeCO = _coroutineRunner.StartCoroutine(TextTypeCO(_lastStatValue, statValue));

        private IEnumerator TextTypeCO(float startValue, float endValue)
        {
            float elapsedTime = 0;

            while (elapsedTime < TextTypeDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / TextTypeDuration;
                float value = Mathf.Lerp(startValue, endValue, t);

                _statVisualizer.SetValue(value);

                yield return null;
            }

            _statVisualizer.SetValue(endValue);
        }

        private void StopTextTypeCO()
        {
            if (_textTypeCO == null)
                return;
            
            _coroutineRunner.StopCoroutine(_textTypeCO);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnItemEquippedEvent() => UpdateStatInfo();

        private void OnItemUnEquippedEvent() => UpdateStatInfo();
    }
}