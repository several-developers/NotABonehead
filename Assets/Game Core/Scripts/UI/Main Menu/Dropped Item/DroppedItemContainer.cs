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
        private void Construct(DiContainer diContainer, IItemsShowcaseService itemsShowcaseService)
        {
            _diContainer = diContainer;
            _itemsShowcaseService = itemsShowcaseService;
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

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() => TryCreateDroppedItemView();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void TryCreateDroppedItemView()
        {
            bool containsDroppedItem = _itemsShowcaseService.ContainsDroppedItem();

            if (!containsDroppedItem)
                return;

            _diContainer.InstantiatePrefab(_droppedItemViewPrefab, _container);
        }
    }
}