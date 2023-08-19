using GameCore.Factories;
using GameCore.Other;
using Zenject;

namespace GameCore.Infrastructure.Installers.Global
{
    public class GameInstaller : MonoInstaller
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindScenesLoader();
            BindMenuFactory();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void BindScenesLoader()
        {
            ScenesLoader scenesLoaderService = FindObjectOfType<ScenesLoader>();

            Container
                .Bind<IScenesLoader>()
                .FromInstance(scenesLoaderService)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindMenuFactory()
        {
            Container
                .Bind<MenuFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}