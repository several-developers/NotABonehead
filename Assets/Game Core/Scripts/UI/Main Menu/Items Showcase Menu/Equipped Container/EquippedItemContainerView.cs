using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    public class EquippedItemContainerView : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IInventoryService inventoryService, IAssetsProvider assetsProvider,
            IItemsShowcaseService itemsShowcaseService)
        {
            _containerLogic = new EquippedItemContainerLogic(inventoryService, assetsProvider, itemsShowcaseService,
                _containerVisualizer, _itemContainer);
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Transform _itemContainer;

        [TitleGroup(Constants.Visualizer)]
        [BoxGroup(Constants.VisualizerIn, showLabel: false), SerializeField]
        private EquippedItemContainerVisualizer _containerVisualizer;

        // FIELDS: --------------------------------------------------------------------------------

        private EquippedItemContainerLogic _containerLogic;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            _containerLogic.UpdateContainerInfo();
    }
}