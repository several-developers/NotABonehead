using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.UI.MainMenu.GameItems
{
    public class WearableItemView : GameItemView
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IInventoryService inventoryService, IAssetsProvider assetsProvider) =>
            _itemLogic = new WearableItemViewLogic(inventoryService, assetsProvider, _itemVisualizer);

        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup(Constants.Visualizer)]
        [BoxGroup(Constants.VisualizerIn, showLabel: false), SerializeField]
        private WearableItemViewVisualizer _itemVisualizer;

        // FIELDS: --------------------------------------------------------------------------------

        private WearableItemViewLogic _itemLogic;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void Setup(ItemViewParams itemParams)
        {
            base.Setup(itemParams);
            _itemLogic.UpdateItemInfo(itemParams);
        }
    }
}