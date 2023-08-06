using GameCore.Enums;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    public class StatItemView : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField]
        private StatType _statType;

        [Title(Constants.References)]
        [SerializeField, Required]
        private TextMeshProUGUI _valueTMP;

        [SerializeField, Required]
        private Image _arrowUp;

        [SerializeField, Required]
        private Image _arrowDown;

        // PROPERTIES: ----------------------------------------------------------------------------

        public StatType StatType => _statType;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetValue(int value) =>
            _valueTMP.text = value.ToString();

        public void SetArrowState(ArrowState arrowState)
        {
            _arrowUp.enabled = arrowState == ArrowState.Up;
            _arrowDown.enabled = arrowState == ArrowState.Down;
        }
    }
}