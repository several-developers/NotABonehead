using System.Collections.Generic;
using GameCore.Items;
using Unity.VisualScripting;
using UnityEngine;

namespace GameCore.Infrastructure.Providers.Global.ItemsMeta
{
    public class ItemsMetaProvider : IItemsMetaProvider
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemsMetaProvider(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
            _itemsMetaDictionary = new Dictionary<string, ItemMeta>(capacity: 16);

            SetupItemDictionary();
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly IAssetsProvider _assetsProvider;
        private readonly Dictionary<string, ItemMeta> _itemsMetaDictionary;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public IEnumerable<ItemMeta> GetAllItemsMeta() =>
            _assetsProvider.GetItemsMeta();
        
        public IEnumerable<WearableItemMeta> GetAllWearableItemsMeta()
        {
            ItemMeta[] itemsMeta = _assetsProvider.GetItemsMeta();
            List<WearableItemMeta> wearableItemsMeta = new(capacity: itemsMeta.Length);

            foreach (ItemMeta itemMeta in itemsMeta)
            {
                if (itemMeta is not WearableItemMeta wearableItemMeta)
                    continue;
                
                wearableItemsMeta.Add(wearableItemMeta);
            }

            return wearableItemsMeta;
        }

        public bool TryGetItemMeta(string itemID, out ItemMeta itemMeta) =>
            _itemsMetaDictionary.TryGetValue(itemID, out itemMeta);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetupItemDictionary()
        {
            ItemMeta[] itemsMeta = _assetsProvider.GetItemsMeta();

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