using GameCore.Battle;
using GameCore.Battle.Monsters;
using GameCore.Battle.Player;
using Zenject;

namespace GameCore.Infrastructure.Installers.BattleScene
{
    public class BattleSceneInstaller : MonoInstaller, ICoroutineRunner
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindCoroutineRunner();
            BindMonsterTracker();
            BindPlayerTracker();
            BindBattleStateController();
            BindVictoryHandler();
            BindDefeatHandler();
            BindGameOverHandler();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }

        private void BindMonsterTracker()
        {
            Container
                .BindInterfacesTo<MonsterTracker>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerTracker()
        {
            Container
                .BindInterfacesTo<PlayerTracker>()
                .AsSingle()
                .NonLazy();
        }

        private void BindBattleStateController()
        {
            Container
                .BindInterfacesTo<BattleStateController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindVictoryHandler()
        {
            Container
                .BindInterfacesTo<DefaultModeVictoryHandler>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindDefeatHandler()
        {
            Container
                .BindInterfacesTo<DefaultModeDefeatHandler>()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameOverHandler()
        {
            Container
                .BindInterfacesTo<GameOverHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}