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

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected void SetDamage(float damage) =>
            _damageTMP.text = damage.ToString(format: "F0");

        protected void PlayAnimation() =>
            _hintAnimation.StartAnimation();
    }
}