using Cysharp.Threading.Tasks;
using GameCore.Configs;
using GameCore.Factories;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.UI.BattleScene.GameOverMenus;
using GameCore.Utilities;

namespace GameCore.Battle
{
    public class DefaultModeVictoryHandler : IVictoryHandler
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public DefaultModeVictoryHandler(IMonstersDataService monstersDataService, IGameDataService gameDataService,
            IConfigsProvider configsProvider)
        {
            _monstersDataService = monstersDataService;
            _gameDataService = gameDataService;
            _battleStageConfig = configsProvider.GetBattleStageConfig();
        }

        // FIELDS: --------------------------------------------------------------------------------
        
        private readonly IMonstersDataService _monstersDataService;
        private readonly IGameDataService _gameDataService;
        private readonly BattleStageConfigMeta _battleStageConfig;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void HandleVictory()
        {
            PlayVictorySound();
            IncreaseMonsterNumber();
            IncreaseLevel();
            CreateVictoryMenu();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void IncreaseMonsterNumber() =>
            _monstersDataService.IncreaseCurrentMonster();
        
        private void IncreaseLevel() =>
            _gameDataService.IncreaseCurrentLevel();
        
        private async void CreateVictoryMenu()
        {
            int delay = _battleStageConfig.GameOverDelay.ConvertToMilliseconds();
            await UniTask.Delay(delay);
            
            MenuFactory.Create<VictoryMenuView>();
        }
        
        private static void PlayVictorySound() =>
            Sounds.PlaySound(SoundType.Victory);
    }
}