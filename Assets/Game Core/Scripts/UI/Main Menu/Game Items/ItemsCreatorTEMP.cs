using GameCore.Enums;
using GameCore.Factories;
using GameCore.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.UI.MainMenu.GameItems
{
    public class ItemsCreatorTEMP : MonoBehaviour
    {
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
    }
}