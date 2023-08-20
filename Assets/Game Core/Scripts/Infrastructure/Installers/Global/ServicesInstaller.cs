using GameCore.Infrastructure.Services.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.Global.Player;
using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using Zenject;

namespace GameCore.Infrastructure.Installers.Global
{
    public partial class ServicesInstaller : MonoInstaller
    {
        // FIELDS: --------------------------------------------------------------------------------

        private DataInstaller _dataInstaller;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindDataServices();
            BindSaveLoadService();
            BindScenesLoaderService();
            BindInventoryService();
            BindRewardsService();
            BindItemsShowcaseService();
            BindPlayerStatsCalculatorService();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindDataServices()
        {
            _dataInstaller = new DataInstaller(Container);
            _dataInstaller.BindDataServices();
        }

        private void BindSaveLoadService()
        {
            Container
                .BindInterfacesTo<SaveLoadService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindScenesLoaderService()
        {
            Container
                .BindInterfacesTo<ScenesLoaderService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindInventoryService()
        {
            Container
                .BindInterfacesTo<InventoryService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindRewardsService()
        {
            Container
                .BindInterfacesTo<RewardsService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindItemsShowcaseService()
        {
            Container
                .BindInterfacesTo<ItemsShowcaseService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerStatsCalculatorService()
        {
            Container
                .BindInterfacesTo<PlayerStatsCalculatorService>()
                .AsSingle()
                .NonLazy();
        }
    }
}