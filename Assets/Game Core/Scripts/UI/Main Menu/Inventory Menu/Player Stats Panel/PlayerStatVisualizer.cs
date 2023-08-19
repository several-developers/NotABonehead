using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GameCore.UI.MainMenu.InventoryMenu.PlayerStatsPanel
{
    [Serializable]
    public class PlayerStatVisualizer
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Required]
        private TextMeshProUGUI _valueTMP;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetValue(float value) =>
            _valueTMP.text = value.ToString(format: "F0");
    }
}