using GameCore.Enums;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.UI.MainMenu.InventoryMenu
{
    public class ItemSlotView : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IInventoryService inventoryService, IConfigsProvider configsProvider) =>
            _itemSlotLogic = new ItemSlotLogic(inventoryService, configsProvider, _itemSlotVisualizer, _slotItemType);

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField]
        private ItemType _slotItemType;

        [TitleGroup(Constants.Visualizer)]
        [BoxGroup(Constants.VisualizerIn, showLabel: false), SerializeField]
        private ItemSlotVisualizer _itemSlotVisualizer;

        // FIELDS: --------------------------------------------------------------------------------

        private ItemSlotLogic _itemSlotLogic;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() => UpdateSlotInfo();

        private void OnDestroy() =>
            _itemSlotLogic.Dispose();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void UpdateSlotInfo() =>
            _itemSlotLogic.UpdateSlotInfo();
         
        // DEBUG BUTTONS: -------------------------------------------------------------------------

        [Title(Constants.DebugButtons)]
        [Button(25)]
        private void DebugUpdateSlotInfo() => UpdateSlotInfo();
    }
}