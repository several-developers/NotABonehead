using GameCore.Battle.Entities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GameCore.UI.BattleScene.DamageHints
{
    public class DamageHint : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private TextMeshProUGUI _damageTMP;

        [SerializeField, Required]
        private HintAnimation _hintAnimation;

        // FIELDS: --------------------------------------------------------------------------------

        protected IEntityTracker EntityTracker;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            EntityTracker.OnTakeDamageEvent += OnTakeDamageEvent;

        private void OnDestroy() =>
            EntityTracker.OnTakeDamageEvent -= OnTakeDamageEvent;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetDamage(float damage) =>
            _damageTMP.text = damage.ToString(format: "F0");

        private void PlayAnimation() =>
            _hintAnimation.StartAnimation();

        // EVENTS RECEIVERS: ----------------------------------------------------------------------
        
        private void OnTakeDamageEvent(float damage)
        {
            SetDamage(damage);
            PlayAnimation();
        }
    }
}