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

        protected void SetDamage(int damage) =>
            _damageTMP.text = damage.ToString();

        protected void PlayAnimation() =>
            _hintAnimation.StartAnimation();
    }
}