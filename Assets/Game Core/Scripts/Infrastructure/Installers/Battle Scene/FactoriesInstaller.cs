using GameCore.Battle.Entities.Monsters;
using GameCore.Battle.Entities.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.Infrastructure.Installers.BattleScene
{
    public class FactoriesInstaller : MonoInstaller
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
            BindMonstersFactory();
            BindPlayerFactory();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void BindMonstersFactory()
        {
            Container
                .BindInterfacesTo<MonstersFactory>()
                .FromInstance(_monstersFactory)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPlayerFactory()
        {
            Container
                .BindInterfacesTo<PlayerFactory>()
                .FromInstance(_playerFactory)
                .AsSingle()
                .NonLazy();
        }
    }
}