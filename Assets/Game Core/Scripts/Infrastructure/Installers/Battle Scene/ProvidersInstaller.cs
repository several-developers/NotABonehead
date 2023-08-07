using GameCore.Infrastructure.Providers.BattleScene.BattleAssets;
using Zenject;

namespace GameCore.Infrastructure.Installers.BattleScene
{
    public class ProvidersInstaller : MonoInstaller
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings() => BindBattleAssets();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindBattleAssets()
        {
            Container
                .BindInterfacesTo<BattleAssetsProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}