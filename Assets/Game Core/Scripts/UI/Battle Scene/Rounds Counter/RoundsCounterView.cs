using GameCore.Battle;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameCore.UI.BattleScene.RoundsCounter
{
    public class RoundsCounterView : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IBattleStateController battleStateController)
        {
            _battleStateController = battleStateController;

            _battleStateController.OnRoundChangedEvent += OnRoundChangedEvent;
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private TextMeshProUGUI _roundTMP;

        // FIELDS: --------------------------------------------------------------------------------
        
        private IBattleStateController _battleStateController;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy() =>
            _battleStateController.OnRoundChangedEvent -= OnRoundChangedEvent;

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnRoundChangedEvent(int currentRound) =>
            _roundTMP.text = $"Round\n{currentRound}";
    }
}
