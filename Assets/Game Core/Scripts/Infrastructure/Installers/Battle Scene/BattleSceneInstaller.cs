using GameCore.Battle;
using GameCore.Battle.Entities;
using Zenject;

namespace GameCore.Infrastructure.Installers.BattleScene
{
    public class BattleSceneInstaller : MonoInstaller
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindMonsterTracker();
            BindPlayerTracker();
            BindVictoryHandler();
            BindDefeatHandler();
            BindGameOverHandler();
            BindBattleStateController();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

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

        private void BindBattleStateController()
        {
            Container
                .BindInterfacesTo<BattleStateController>()
                .AsSingle()
                .NonLazy();
        }
    }
}