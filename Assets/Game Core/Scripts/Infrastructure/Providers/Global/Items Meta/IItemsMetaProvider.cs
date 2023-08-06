using System.Collections.Generic;
using GameCore.Items;

namespace GameCore.Infrastructure.Providers.Global.ItemsMeta
{
    public interface IItemsMetaProvider
    {
        IEnumerable<ItemMeta> GetAllItemsMeta();
        IEnumerable<WearableItemMeta> GetAllWearableItemsMeta();
        bool TryGetItemMeta(string itemID, out ItemMeta itemMeta);
    }
}