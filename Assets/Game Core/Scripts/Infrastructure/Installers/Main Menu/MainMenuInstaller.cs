using GameCore.Factories;
using Zenject;

namespace GameCore.Infrastructure.Installers.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindGameItemsFactory();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindGameItemsFactory()
        {
            Container
                .BindInterfacesAndSelfTo<GameItemsFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}