using GameCore.Enums;
using GameCore.Other;
using GameCore.UI.Global.Buttons;
using Zenject;

namespace GameCore.UI.MainMenu.HUD
{
    public class BattleButton : BaseButton
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------
        
        [Inject]
        private void Construct(IScenesLoader scenesLoader) =>
            _scenesLoader = scenesLoader;

        // FIELDS: --------------------------------------------------------------------------------
        
        private IScenesLoader _scenesLoader;
        
        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected override void ClickLogic() =>
            _scenesLoader.LoadScene(SceneName.Battle);
    }
}
