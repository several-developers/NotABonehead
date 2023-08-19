using GameCore.Enums;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.Global.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.UI.MainMenu.InventoryMenu.PlayerStatsPanel
{
    public class PlayerStatView : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IInventoryService inventoryService,
            IPlayerStatsCalculatorService playerStatsCalculatorService)
        {
            _statLogic = new PlayerStatLogic(inventoryService, playerStatsCalculatorService, _statVisualizer,
                coroutineRunner: this, _statType);
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField]
        private StatType _statType;

        [TitleGroup(Constants.Visualizer)]
        [BoxGroup(Constants.VisualizerIn, showLabel: false), SerializeField]
        private PlayerStatVisualizer _statVisualizer;

        // FIELDS: --------------------------------------------------------------------------------

        private PlayerStatLogic _statLogic;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            _statLogic.UpdateStatInfo(instant: true);

        private void OnDestroy() =>
            _statLogic.Dispose();
    }
}