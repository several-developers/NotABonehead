using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    public class DroppedItemContainerView : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IInventoryService inventoryService, IAssetsProvider assetsProvider)
        {
            _containerLogic =
                new DroppedItemContainerLogic(inventoryService, assetsProvider, _containerVisualizer, _itemContainer);
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Transform _itemContainer;

        [TitleGroup(Constants.Visualizer)]
        [BoxGroup(Constants.VisualizerIn, showLabel: false), SerializeField]
        private EquippedItemContainerVisualizer _containerVisualizer;

        // FIELDS: --------------------------------------------------------------------------------

        private DroppedItemContainerLogic _containerLogic;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            _containerLogic.UpdateContainerInfo();
    }
}