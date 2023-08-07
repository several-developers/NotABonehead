using GameCore.Battle;
using GameCore.Battle.Player;
using GameCore.Battle.Monsters;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.Infrastructure.Installers.BattleScene
{
    public class BattleSceneInstaller : MonoInstaller, ICoroutineRunner
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private MonstersFactory _monstersFactory;
        
        [SerializeField, Required]
        private PlayerFactory _playerFactory;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindCoroutineRunner();
            BindMonsterTracker();
            BindMonstersFactory();
            BindPlayerTracker();
            BindPlayerFactory();
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

        private void BindMonstersFactory()
        {
            Container
                .BindInterfacesTo<MonstersFactory>()
                .FromInstance(_monstersFactory)
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

        private void BindPlayerFactory()
        {
            Container
                .Bind<IPlayerFactory>()
                .FromInstance(_playerFactory)
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