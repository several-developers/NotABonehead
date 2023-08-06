using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.MainMenu.GameItems
{
    [Serializable]
    public class WearableItemViewVisualizer
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Required]
        private Image _iconImage;
        
        [SerializeField, Required]
        private Image _frameImage;

        [SerializeField, Required]
        private TextMeshProUGUI _levelTMP;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetItemIcon(Sprite sprite) =>
            _iconImage.sprite = sprite;

        public void SetFrameImage(Sprite sprite) =>
            _frameImage.sprite = sprite;

        public void SetItemLevel(int level) =>
            _levelTMP.text = $"Lv. {level}";
    }
}