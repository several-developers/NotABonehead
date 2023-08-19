using Cysharp.Threading.Tasks;
using GameCore.Configs;
using GameCore.Factories;
using GameCore.Infrastructure.Providers.Global;
using GameCore.UI.BattleScene.GameOverMenus;
using GameCore.Utilities;

namespace GameCore.Battle
{
    public class DefaultModeDefeatHandler : IDefeatHandler
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public DefaultModeDefeatHandler(IConfigsProvider configsProvider)
        {
            _battleStageConfig = configsProvider.GetBattleStageConfig();
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly BattleStageConfigMeta _battleStageConfig;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void HandleDefeat()
        {
            PlayDefeatSound();
            CreateDefeatMenu();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void CreateDefeatMenu()
        {
            int delay = _battleStageConfig.GameOverDelay.ConvertToMilliseconds();
            await UniTask.Delay(delay);
            
            MenuFactory.Create<DefeatMenuView>();
        }
        
        private static void PlayDefeatSound() =>
            Sounds.PlaySound(SoundType.Defeat);
    }
}