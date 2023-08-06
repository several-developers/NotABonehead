using GameCore.Items;
using UnityEngine;

namespace GameCore.UI.MainMenu.GameItems
{
    [DisallowMultipleComponent]
    public abstract class GameItemView : MonoBehaviour, IComplexGameItemView<ItemViewParams>
    {
        // FIELDS: --------------------------------------------------------------------------------
        
        protected ItemViewParams ItemParams;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public virtual void Setup(ItemViewParams itemParams) =>
            ItemParams = itemParams;
    }
}