namespace GameCore.Items
{
    public class ItemLevelParam : IItemViewParam<int>
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemLevelParam()
        {
        }
        
        public ItemLevelParam(int itemLevel) =>
            _itemLevel = itemLevel;

        // FIELDS: --------------------------------------------------------------------------------
        
        private int _itemLevel = 1;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void SetValue(int itemLevel) =>
            _itemLevel = itemLevel;

        public int GetValue() => _itemLevel;
    }
}