using GameCore.UI.Global.MenuView;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    public class ItemsShowcaseMenuView : MenuView
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _closeButton;
        
        [SerializeField, Required]
        private Button _overlayCloseButton;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseClicked);
            _overlayCloseButton.onClick.AddListener(OnCloseClicked);
            
            DestroyOnHide();
        }

        private void Start() => Show();

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnCloseClicked() => Hide();
    }
}