using GameCore.Enums;

namespace GameCore.Infrastructure.Services.MainMenu.ItemsShowcase
{
    public interface IItemsShowcaseService
    {
        ItemType GetSelectedItemType();
        bool ContainsDroppedItem();
    }
}