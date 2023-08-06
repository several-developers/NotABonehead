using GameCore.UI.Global.MenuView;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.MainMenu.ItemsDropChancesMenu
{
    public class ItemsDropChancesMenuView : MenuView
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _closeButton;
        
        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseClicked);
            
            DestroyOnHide();
        }

        private void Start() => Show();

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnCloseClicked() => Hide();
    }
}