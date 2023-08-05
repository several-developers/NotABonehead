using GameCore.Items;

namespace GameCore.Infrastructure.Providers.Global.ItemsMeta
{
    public interface IItemsMetaProvider
    {
        bool TryGetItemMeta(string itemID, out ItemMeta itemMeta);
    }
}