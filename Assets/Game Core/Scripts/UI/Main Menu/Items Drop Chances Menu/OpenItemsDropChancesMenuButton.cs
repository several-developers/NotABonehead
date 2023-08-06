using GameCore.Factories;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.MainMenu.ItemsDropChancesMenu
{
    public class OpenItemsDropChancesMenuButton : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _button;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            _button.onClick.AddListener(OnButtonClicked);

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private static void OnButtonClicked() =>
            MenuFactory.Create<ItemsDropChancesMenuView>();
    }
}