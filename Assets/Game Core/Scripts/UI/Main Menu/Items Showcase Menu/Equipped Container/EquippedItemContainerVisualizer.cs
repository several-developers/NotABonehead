using System;
using GameCore.Enums;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    [Serializable]
    public class EquippedItemContainerVisualizer
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Required]
        private GameObject _container;
        
        [SerializeField, Required]
        private TextMeshProUGUI _itemNameTMP;
        
        [SerializeField, Required]
        private TextMeshProUGUI _notEquippedTMP;
        
        [SerializeField, Required, Space(5)]
        private StatItemView[] _statItemsView;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetItemName(ItemRarity itemRarity, string itemName) =>
            _itemNameTMP.text = $"[{itemRarity}] {itemName}";

        public void SetItemNameColor(Color color) =>
            _itemNameTMP.color = color;

        public void SetEquippedState(bool isEquipped)
        {
            _notEquippedTMP.enabled = !isEquipped;
            _container.SetActive(isEquipped);
        }

        public void SetStatValue(StatType statType, int value)
        {
            foreach (StatItemView statItemView in _statItemsView)
            {
                if (statItemView.StatType != statType)
                    continue;
                
                statItemView.SetValue(value);
                break;
            }
        }

        public void SetArrowState(StatType statType, ArrowState arrowState)
        {
            foreach (StatItemView statItemView in _statItemsView)
            {
                if (statItemView.StatType != statType)
                    continue;
                
                statItemView.SetArrowState(arrowState);
                break;
            }
        }
    }
}