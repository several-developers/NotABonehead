using System.Collections;
using GameCore.Enums;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.Global.Player;
using UnityEngine;

namespace GameCore.UI.MainMenu.InventoryMenu.PlayerStatsPanel
{
    public class PlayerStatLogic
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public PlayerStatLogic(IInventoryService inventoryService,
            IPlayerStatsCalculatorService playerStatsCalculatorService, PlayerStatVisualizer statVisualizer,
            MonoBehaviour coroutineRunner, StatType statType)
        {
            _inventoryService = inventoryService;
            _playerStatsCalculatorService = playerStatsCalculatorService;
            _statVisualizer = statVisualizer;
            _coroutineRunner = coroutineRunner;
            _statType = statType;

            _inventoryService.OnItemEquippedEvent += OnItemEquippedEvent;
            _inventoryService.OnItemUnEquippedEvent += OnItemUnEquippedEvent;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private const float TextTypeDuration = 0.5f;

        private readonly IInventoryService _inventoryService;
        private readonly IPlayerStatsCalculatorService _playerStatsCalculatorService;
        private readonly PlayerStatVisualizer _statVisualizer;
        private readonly MonoBehaviour _coroutineRunner;
        private readonly StatType _statType;

        private Coroutine _textTypeCO;
        private float _lastStatValue;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void UpdateStatInfo(bool instant = false)
        {
            float statValue = _playerStatsCalculatorService.GetPlayerStatValue(_statType);

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

        private void UpdateStatWithAnimation(float statValue) =>
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