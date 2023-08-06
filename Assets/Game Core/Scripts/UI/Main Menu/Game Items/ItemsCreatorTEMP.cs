using GameCore.Enums;
using GameCore.Factories;
using GameCore.Infrastructure.Services.Global.Rewards;
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
        private void Construct(IRewardsService rewardsService) =>
            _rewardsService = rewardsService;

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Required]
        private ItemMeta _itemMeta;
        
        [SerializeField]
        private ItemStats _itemStats;
        
        // FIELDS: --------------------------------------------------------------------------------

        private IRewardsService _rewardsService;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static void CreateItemsShowcaseMenu() =>
            MenuFactory.Create<ItemsShowcaseMenuView>();

        [Button(35), DisableInEditorMode]
        private void GiveItemReward()
        {
            if (_itemMeta == null)
                return;
            
            _rewardsService.GiveItem(_itemMeta.ItemID, _itemStats);
            CreateItemsShowcaseMenu();
        }

        [Button(35), DisableInEditorMode]
        private void GiveRandomItemReward()
        {
            _rewardsService.GiveRandomItem();
            CreateItemsShowcaseMenu();
        }

        [Button(35, ButtonStyle.FoldoutButton), DisableInEditorMode]
        private void GiveItemReward(ItemType itemType)
        {
            _rewardsService.GiveItem(itemType, _itemStats.Rarity);
            CreateItemsShowcaseMenu();
        }
    }
}