using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using GameCore.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI.MainMenu.DroppedItem
{
    public class DroppedItemView : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IInventoryService inventoryService, IItemsShowcaseService itemsShowcaseService)
        {
            _inventoryService = inventoryService;
            _itemsShowcaseService = itemsShowcaseService;
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Image _itemIconImage;
        
        [TitleGroup("Animation")]
        [BoxGroup("Animation/In", showLabel: false), SerializeField]
        private DroppedItemAnimation _droppedItemAnimation;

        // FIELDS: --------------------------------------------------------------------------------

        private IInventoryService _inventoryService;
        private IItemsShowcaseService _itemsShowcaseService;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start()
        {
            _droppedItemAnimation.StartAnimation();
            UpdateIcon();
        }

        private void OnDestroy() =>
            _droppedItemAnimation.Dispose();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void UpdateIcon()
        {
            bool containsDroppedItem = _itemsShowcaseService.ContainsDroppedItem();

            if (!containsDroppedItem)
                return;
            
            _inventoryService.TryGetDroppedItemData(out ItemData itemData);
            _inventoryService.TryGetItemMetaByID(itemData.ItemID, out ItemMeta itemMeta);

            _itemIconImage.sprite = itemMeta.Icon;
        }
    }
}