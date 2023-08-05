using System.Collections.Generic;
using GameCore.Items;
using UnityEngine;

namespace GameCore.Infrastructure.Providers.Global.ItemsMeta
{
    public class ItemsMetaProvider : IItemsMetaProvider
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemsMetaProvider(IAssetsProvider assetsProvider)
        {
            _itemsMetaDictionary = new Dictionary<string, ItemMeta>(capacity: 16);

            SetupItemDictionary(assetsProvider);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly Dictionary<string, ItemMeta> _itemsMetaDictionary;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public bool TryGetItemMeta(string itemID, out ItemMeta itemMeta) =>
            _itemsMetaDictionary.TryGetValue(itemID, out itemMeta);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetupItemDictionary(IAssetsProvider assetsProvider)
        {
            ItemMeta[] itemsMeta = assetsProvider.GetItemsMeta();

            foreach (ItemMeta itemMeta in itemsMeta)
            {
                string itemID = itemMeta.ItemID;

                if (_itemsMetaDictionary.ContainsKey(itemID))
                {
                    Debug.LogError($"Dictionary already contains item with ID: {itemID}");
                    continue;
                }
                
                _itemsMetaDictionary.Add(itemID, itemMeta);
            }
        }
    }
}