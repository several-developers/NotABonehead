using GameCore.Enums;

namespace GameCore.Items
{
    public class ItemRarityParam : IItemViewParam<ItemRarity>
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemRarityParam(ItemRarity itemRarity) =>
            _itemRarity = itemRarity;

        // FIELDS: --------------------------------------------------------------------------------
        
        private ItemRarity _itemRarity;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void SetValue(ItemRarity itemRarity) =>
            _itemRarity = itemRarity;

        public ItemRarity GetValue() => _itemRarity;
    }
}