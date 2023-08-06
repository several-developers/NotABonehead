using GameCore.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.MainMenu.GameItems
{
    [DisallowMultipleComponent]
    public abstract class GameItemView : MonoBehaviour, IComplexGameItemView<ItemViewParams>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _button;
        
        // FIELDS: --------------------------------------------------------------------------------
        
        protected ItemViewParams ItemParams;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public virtual void Setup(ItemViewParams itemParams)
        {
            ItemParams = itemParams;
            CheckInteractableState();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CheckInteractableState()
        {
            bool isInteractable = ItemParams.IsInteractable;
            _button.interactable = isInteractable;
        }
    }
}