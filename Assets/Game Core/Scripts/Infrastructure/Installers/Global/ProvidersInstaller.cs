using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Providers.Global.Data;
using GameCore.Infrastructure.Providers.Global.ItemsMeta;
using Zenject;

namespace GameCore.Infrastructure.Installers.Global
{
    public class ProvidersInstaller : MonoInstaller
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindData();
            BindAssets();
            BindItemsMeta();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindData()
        {
            Container
                .BindInterfacesTo<DataProvider>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindAssets()
        {
            Container
                .BindInterfacesTo<AssetsProvider>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindItemsMeta()
        {
            Container
                .BindInterfacesTo<ItemsMetaProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}