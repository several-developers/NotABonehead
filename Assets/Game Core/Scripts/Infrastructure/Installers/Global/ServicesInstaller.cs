using GameCore.Infrastructure.Services.Global;
using GameCore.Infrastructure.Services.Global.Data;
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
        }
    }
}