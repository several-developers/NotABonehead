using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    [Serializable]
    public class ItemsShowcaseMenuVisualizer
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Required]
        private GameObject _singleEquipButton;

        [SerializeField, Required]
        private GameObject _equipButton;
        
        [SerializeField, Required]
        private GameObject _dropButton;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetItemEquippedState(bool isItemEquipped)
        {
            _singleEquipButton.SetActive(!isItemEquipped);
            _equipButton.SetActive(isItemEquipped);
            _dropButton.SetActive(isItemEquipped);
        }
    }
}