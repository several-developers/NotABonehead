using GameCore.Infrastructure.Providers.Global.Data;
using Zenject;

namespace GameCore.Infrastructure.Installers.Global
{
    public class ProvidersInstaller : MonoInstaller
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindData();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindData()
        {
            Container
                .BindInterfacesTo<DataProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}