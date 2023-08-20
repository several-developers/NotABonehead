using GameCore.Factories;
using GameCore.MainMenu;
using Zenject;

namespace GameCore.Infrastructure.Installers.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindGameItemsFactory();
            BindPlayerPreviewObserver();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindGameItemsFactory()
        {
            Container
                .BindInterfacesAndSelfTo<GameItemsFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerPreviewObserver()
        {
            Container
                .BindInterfacesTo<PlayerPreviewObserver>()
                .AsSingle()
                .NonLazy();
        }
    }
}