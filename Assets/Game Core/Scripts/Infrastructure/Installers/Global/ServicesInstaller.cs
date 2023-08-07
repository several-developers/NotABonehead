using GameCore.Factories;
using GameCore.Infrastructure.Services.Global;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using GameCore.Other;
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
            BindScenesLoader();
            BindMenuFactory();
            BindItemsShowcase();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindMenuFactory()
        {
            Container
                .Bind<MenuFactory>()
                .AsSingle()
                .NonLazy();
        }
        
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
        
        private void BindScenesLoader()
        {
            ScenesLoader scenesLoaderService = FindObjectOfType<ScenesLoader>();

            Container
                .Bind<IScenesLoader>()
                .FromInstance(scenesLoaderService)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindItemsShowcase()
        {
            Container
                .BindInterfacesTo<ItemsShowcaseService>()
                .AsSingle()
                .NonLazy();
        }
    }
}