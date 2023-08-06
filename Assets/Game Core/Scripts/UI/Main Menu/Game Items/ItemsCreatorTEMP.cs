using GameCore.Enums;
using GameCore.Factories;
using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using GameCore.Items;
using GameCore.UI.MainMenu.ItemsShowcaseMenu;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.UI.MainMenu.GameItems
{
    public class ItemsCreatorTEMP : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IRewardsService rewardsService, IItemsShowcaseService itemsShowcaseService)
        {
            _rewardsService = rewardsService;
            _itemsShowcaseService = itemsShowcaseService;
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField]
        private ItemRarity _itemRarity;

        [SerializeField, Min(1)]
        private int _itemLevel = 1;

        [SerializeField, Min(1)]
        private float _destroyDelay = 3f;

        [Title(Constants.References)]
        [SerializeField]
        private WearableItemMeta _itemMeta;

        // FIELDS: --------------------------------------------------------------------------------

        private IRewardsService _rewardsService;
        private IItemsShowcaseService _itemsShowcaseService;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        [Button(35)]
        private void CreateItem()
        {
            if (_itemMeta == null)
                return;

            ItemViewParams itemViewParams = new(_itemMeta.ItemID, useItemKey: false, isInteractable: true);

            ItemRarityParam itemRarityParam = new(_itemRarity);
            ItemLevelParam itemLevelParam = new(_itemLevel);
            
            itemViewParams.AddParam(itemRarityParam);
            itemViewParams.AddParam(itemLevelParam);

            GameItemView itemInstance =
                GameItemsFactory.Create<WearableItemView, ItemViewParams>(transform, itemViewParams);
            
            Destroy(itemInstance.gameObject, _destroyDelay);
        }

        [Button(35)]
        private void GiveRandomItemReward()
        {
            bool isSuccessful = _rewardsService.GiveRandomItemReward(transform, out GameItemView gameItemView);
            
            if (!isSuccessful)
                return;
            
            Destroy(gameItemView.gameObject, _destroyDelay);
        }

        [Button(35, ButtonStyle.FoldoutButton)]
        private void GiveItemReward(ItemType itemType)
        {
            bool isSuccessful = _rewardsService.GiveItemReward(transform, itemType, _itemRarity, out GameItemView gameItemView);

            if (!isSuccessful)
                return;
            
            Destroy(gameItemView.gameObject, _destroyDelay);
        }

        [Button(35, ButtonStyle.FoldoutButton)]
        private void SelectItem(ItemType itemType)
        {
            _itemsShowcaseService.SetSelectedItemType(itemType);
            MenuFactory.Create<ItemsShowcaseMenuView>();
        }
    }
}