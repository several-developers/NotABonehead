using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.BattleScene.HealthBars
{
    public class BaseBarView : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _fillTime = 0.5f;

        [SerializeField]
        private Ease _fillEase;

        [Title(Constants.References)]
        [SerializeField, Required]
        private Slider _fillSlider;

        // FIELDS: --------------------------------------------------------------------------------

        private Tweener _fillTN;

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected void SetFill(float value)
        {
            value = Mathf.Clamp01(value);
            
            _fillTN.Kill();

            _fillTN = _fillSlider
                .DOValue(value, _fillTime)
                .SetEase(_fillEase)
                .SetLink(gameObject);
        }
    }
}
