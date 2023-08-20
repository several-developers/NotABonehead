using GameCore.Battle;
using GameCore.Factories;
using Zenject;

namespace GameCore.Infrastructure.Installers.Global
{
    public class GameInstaller : MonoInstaller, ICoroutineRunner
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override void InstallBindings()
        {
            BindCoroutineRunner();
            BindMenuFactory();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(this)
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