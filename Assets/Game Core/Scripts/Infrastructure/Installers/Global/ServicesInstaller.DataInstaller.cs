using GameCore.Infrastructure.Services.Global.Data;
using Zenject;

namespace GameCore.Infrastructure.Installers.Global
{
    public partial class ServicesInstaller
    {
        public class DataInstaller
        {
            // CONSTRUCTORS: --------------------------------------------------------------------------

            public DataInstaller(DiContainer container) =>
                _container = container;

            // FIELDS: --------------------------------------------------------------------------------

            private readonly DiContainer _container;
            
            // PUBLIC METHODS: ------------------------------------------------------------------------

            public void BindDataServices()
            {
                BindGameData();
                BindPlayerData();
                BindGameSettingsData();
                BindInventoryData();
                BindMonstersData();
            }

            // PRIVATE METHODS: -----------------------------------------------------------------------
            
            private void BindGameData()
            {
                _container
                    .BindInterfacesTo<GameDataService>()
                    .AsSingle()
                    .NonLazy();
            }

            private void BindPlayerData()
            {
                _container
                    .BindInterfacesTo<PlayerDataService>()
                    .AsSingle()
                    .NonLazy();
            }

            private void BindGameSettingsData()
            {
                _container
                    .BindInterfacesTo<GameSettingsDataService>()
                    .AsSingle()
                    .NonLazy();
            }

            private void BindInventoryData()
            {
                _container
                    .BindInterfacesTo<InventoryDataService>()
                    .AsSingle()
                    .NonLazy();
            }

            private void BindMonstersData()
            {
                _container
                    .BindInterfacesTo<MonstersDataService>()
                    .AsSingle()
                    .NonLazy();
            }
        }
    }
}