using Cysharp.Threading.Tasks;
using GameCore.Configs;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using GameCore.Utilities;
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
            IInventoryService inventoryService, IConfigsProvider configsProvider)
        {
            _diContainer = diContainer;
            _itemsShowcaseService = itemsShowcaseService;
            _inventoryService = inventoryService;
            _gameConfig = configsProvider.GetGameConfig();

            _inventoryService.OnReceivedDroppedItemEvent += OnReceivedDroppedItemEvent;
        }

        // MEMBERS: -------------------------------------------------------------------------------
        
        [Title(Constants.References)]
        [SerializeField, Required]
        private Transform _container;

        [SerializeField, Required]
        private DroppedItemView _droppedItemViewPrefab;

        // FIELDS: --------------------------------------------------------------------------------

        private DiContainer _diContainer;
        private IItemsShowcaseService _itemsShowcaseService;
        private IInventoryService _inventoryService;
        private GameConfigMeta _gameConfig;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() => TryCreateDroppedItemView();

        private void OnDestroy() =>
            _inventoryService.OnReceivedDroppedItemEvent -= OnReceivedDroppedItemEvent;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void TryCreateDroppedItemViewWithDelay()
        {
            int delay = _gameConfig.DroppedItemCreateDelay.ConvertToMilliseconds();
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