using System;
using Coffee.UIEffects;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.MainMenu.InventoryMenu
{
    [Serializable]
    public class ItemSlotVisualizer
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Color _disabledColor = Color.white;
        
        [SerializeField, Required]
        private Image _iconImage;
        
        [SerializeField, Required]
        private Image _disabledFrameImage;
        
        [SerializeField, Required]
        private Image _frameImage;

        [SerializeField, Required]
        private TextMeshProUGUI _itemLevelTMP;

        [SerializeField, Required]
        private UIEffect _iconUIEffect;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetItemIcon(Sprite sprite) =>
            _iconImage.sprite = sprite;

        public void SetFrameImage(Sprite sprite) =>
            _frameImage.sprite = sprite;

        public void SetItemLevel(int itemLevel) =>
            _itemLevelTMP.text = $"Lv. {itemLevel}";

        public void SetItemAvailableState(bool isAvailable)
        {
            Color color = isAvailable ? Color.white : _disabledColor;

            _iconImage.color = color;
            _itemLevelTMP.enabled = isAvailable;
            _disabledFrameImage.enabled = !isAvailable;
            _frameImage.enabled = isAvailable;
            _iconUIEffect.enabled = !isAvailable;
        }
    }
}