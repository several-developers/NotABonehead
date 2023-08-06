using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using Zenject;

namespace GameCore.Infrastructure.Installers.MainMenu
{
    public class ServiceInstaller : MonoInstaller
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings() => BindItemsShowcase();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindItemsShowcase()
        {
            Container
                .BindInterfacesTo<ItemsShowcaseService>()
                .AsSingle()
                .NonLazy();
        }
    }
}