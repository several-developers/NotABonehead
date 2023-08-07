using GameCore.Infrastructure.Services.Global;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.Global.Rewards;
using Zenject;

namespace GameCore.Infrastructure.Installers.Global
{
    public class ServicesInstaller : MonoInstaller
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindSaveLoad();
            BindAllData();
            BindInventory();
            BindRewards();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindSaveLoad()
        {
            Container
                .BindInterfacesTo<SaveLoadService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAllData()
        {
            BindGameData();
            BindPlayerData();
            BindGameSettingsData();
            BindInventoryData();
            BindMonstersData();
            
            // METHODS: -----------------------------------

            void BindGameData()
            {
                Container
                    .BindInterfacesTo<GameDataService>()
                    .AsSingle()
                    .NonLazy();
            }
            
            void BindPlayerData()
            {
                Container
                    .BindInterfacesTo<PlayerDataService>()
                    .AsSingle()
                    .NonLazy();
            }
            
            void BindGameSettingsData()
            {
                Container
                    .BindInterfacesTo<GameSettingsDataService>()
                    .AsSingle()
                    .NonLazy();
            }
            
            void BindInventoryData()
            {
                Container
                    .BindInterfacesTo<InventoryDataService>()
                    .AsSingle()
                    .NonLazy();
            }
            
            void BindMonstersData()
            {
                Container
                    .BindInterfacesTo<MonstersDataService>()
                    .AsSingle()
                    .NonLazy();
            }
        }

        private void BindInventory()
        {
            Container
                .BindInterfacesTo<InventoryService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindRewards()
        {
            Container
                .BindInterfacesTo<RewardsService>()
                .AsSingle()
                .NonLazy();
        }
    }
}