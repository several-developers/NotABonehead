using GameCore.Factories;
using GameCore.UI.Global.Buttons;
using GameCore.UI.MainMenu.GameSettingsMenu;

namespace GameCore.UI.MainMenu.HUD
{
    public class SettingsButton : BaseButton
    {
        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected override void ClickLogic() =>
            MenuFactory.Create<GameSettingsMenuView>();
    }
}