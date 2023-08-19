using System;
using GameCore.Enums;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    [Serializable]
    public class StatItemVisualizer
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Color _redColor;
        
        [SerializeField]
        private Color _greenColor;

        [SerializeField, Required]
        private TextMeshProUGUI _differenceTMP;
        
        [SerializeField, Required]
        private TextMeshProUGUI _valueTMP;
        
        [SerializeField, Required]
        private Image _arrowUp;

        [SerializeField, Required]
        private Image _arrowDown;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetValue(float value) =>
            _valueTMP.text = value.ToString(format: "F0");
        
        public void SetDifference(float value)
        {
            bool useGreenColor = value >= 0;
            Color color = useGreenColor ? _greenColor : _redColor;
            
            _differenceTMP.color = color;
            _differenceTMP.text = $"{(value > 0 ? "+" : "")}{value:F0}";
        }

        public void SetDifferenceVisibilityState(ArrowState arrowState)
        {
            bool showDifference = arrowState != ArrowState.Hidden;
            _differenceTMP.enabled = showDifference;
        }
        
        public void SetArrowState(ArrowState arrowState)
        {
            _arrowUp.enabled = arrowState == ArrowState.Up;
            _arrowDown.enabled = arrowState == ArrowState.Down;
        }
    }
    public class StatItemView : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField]
        private StatType _statType;

        [TitleGroup(Constants.Visualizer)]
        [BoxGroup(Constants.VisualizerIn, showLabel: false), SerializeField]
        private StatItemVisualizer _itemVisualizer;

        // PROPERTIES: ----------------------------------------------------------------------------

        public StatType StatType => _statType;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetValue(float value) =>
            _itemVisualizer.SetValue(value);

        public void SetArrowState(ArrowState arrowState)
        {
            _itemVisualizer.SetArrowState(arrowState);
            _itemVisualizer.SetDifferenceVisibilityState(arrowState);
        }

        public void SetDifference(float value) =>
            _itemVisualizer.SetDifference(value);
    }
}