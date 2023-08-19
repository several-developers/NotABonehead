using Cysharp.Threading.Tasks;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.UI.MainMenu.DroppedItem
{
    public class DroppedItemContainer : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(DiContainer diContainer, IItemsShowcaseService itemsShowcaseService,
            IInventoryService inventoryService)
        {
            _diContainer = diContainer;
            _itemsShowcaseService = itemsShowcaseService;
            _inventoryService = inventoryService;

            _inventoryService.OnReceivedDroppedItemEvent += OnReceivedDroppedItemEvent;
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _createDelay = 0.5f;
        
        [Title(Constants.References)]
        [SerializeField, Required]
        private Transform _container;

        [SerializeField, Required]
        private DroppedItemView _droppedItemViewPrefab;

        // FIELDS: --------------------------------------------------------------------------------

        private DiContainer _diContainer;
        private IItemsShowcaseService _itemsShowcaseService;
        private IInventoryService _inventoryService;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() => TryCreateDroppedItemView();

        private void OnDestroy() =>
            _inventoryService.OnReceivedDroppedItemEvent -= OnReceivedDroppedItemEvent;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void TryCreateDroppedItemViewWithDelay()
        {
            int delay = (int)(_createDelay * 1000);
            bool isCanceled = await UniTask
                .Delay(delay, cancellationToken: this.GetCancellationTokenOnDestroy())
                .SuppressCancellationThrow();

            if (isCanceled)
                return;
            
            TryCreateDroppedItemView();
        }
        
        private void TryCreateDroppedItemView()
        {
            bool containsDroppedItem = _itemsShowcaseService.ContainsDroppedItem();

            if (!containsDroppedItem)
                return;

            _diContainer.InstantiatePrefab(_droppedItemViewPrefab, _container);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnReceivedDroppedItemEvent() => TryCreateDroppedItemViewWithDelay();
    }
}