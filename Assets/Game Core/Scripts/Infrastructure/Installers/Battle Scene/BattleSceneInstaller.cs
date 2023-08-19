using GameCore.Battle;
using GameCore.Battle.Player;
using GameCore.Battle.Monsters;
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
    }
}