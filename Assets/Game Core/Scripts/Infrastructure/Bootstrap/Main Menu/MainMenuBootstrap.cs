using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using UnityEngine;
using Zenject;

namespace GameCore.Infrastructure.Bootstrap.MainMenu
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------
        
        [Inject]
        private void Construct(IItemsShowcaseService itemsShowcaseService) =>
            _itemsShowcaseService = itemsShowcaseService;

        // FIELDS: --------------------------------------------------------------------------------
        
        private IItemsShowcaseService _itemsShowcaseService;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Start() =>
            _itemsShowcaseService.CheckIfContainsDroppedItem();
    }
}