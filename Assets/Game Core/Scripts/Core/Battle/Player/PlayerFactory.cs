using System.Collections.Generic;
using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Services.Global.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.Battle.Player
{
    public class PlayerFactory : MonoBehaviour, IPlayerFactory
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(DiContainer diContainer, IPlayerTracker playerTracker,
            IInventoryService inventoryService)
        {
            _diContainer = diContainer;
            _playerTracker = playerTracker;
            _inventoryService = inventoryService;
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private int _health = 10;

        [SerializeField, Min(0)]
        private int _damage = 1;

        [SerializeField, Min(0)]
        private int _defense;

        [Title(Constants.References)]
        [SerializeField, Required]
        private GameObject _playerPrefab;

        // FIELDS: --------------------------------------------------------------------------------

        private DiContainer _diContainer;
        private IPlayerTracker _playerTracker;
        private IInventoryService _inventoryService;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Create()
        {
            GameObject playerInstance = _diContainer.InstantiatePrefab(_playerPrefab, transform);
            PlayerBrain monsterBrain = playerInstance.AddComponent<PlayerBrain>();

            int health = CalculateStat(StatType.Health);
            int damage = CalculateStat(StatType.Damage);
            int defense = CalculateStat(StatType.Defense);

            health += _health;
            damage += _damage;
            defense += _defense;

            monsterBrain.Setup(_playerTracker, health, damage, defense);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private int CalculateStat(StatType statType)
        {
            IEnumerable<string> allEquippedItemsKeys = _inventoryService.GetAllEquippedItemsKeys();
            int statValue = 0;

            foreach (string itemKey in allEquippedItemsKeys)
            {
                bool isItemDataFound = _inventoryService.TryGetItemData(itemKey, out ItemData itemData);

                if (!isItemDataFound)
                    continue;

                switch (statType)
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
    }
}