using GameCore.Infrastructure.Services.Global;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.Infrastructure.Services.Global.Inventory;
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
        }

        private void BindInventory()
        {
            Container
                .BindInterfacesTo<InventoryService>()
                .AsSingle()
                .NonLazy();
        }
    }
}