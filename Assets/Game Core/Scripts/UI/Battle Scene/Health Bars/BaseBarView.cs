using DG.Tweening;
using GameCore.Battle.Entities;
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

        protected IEntityTracker EntityTracker;
        
        private Tweener _fillTN;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            EntityTracker.OnHealthChangedEvent += OnHealthChangedEvent;

        private void OnDestroy() =>
            EntityTracker.OnHealthChangedEvent -= OnHealthChangedEvent;

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void SetFill(float value)
        {
            value = Mathf.Clamp01(value);
            
            _fillTN.Kill();

            _fillTN = _fillSlider
                .DOValue(value, _fillTime)
                .SetEase(_fillEase)
                .SetLink(gameObject);
        }
        
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnHealthChangedEvent(float currentHealth, float maxHealth)
        {
            float fillValue = Mathf.Clamp01(currentHealth / maxHealth);
            SetFill(fillValue);
        }
    }
}
