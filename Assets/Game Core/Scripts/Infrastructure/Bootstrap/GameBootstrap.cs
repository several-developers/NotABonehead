using GameCore.Infrastructure.Providers.Global;
using UnityEngine;
using Zenject;

namespace GameCore.Infrastructure.Bootstrap
{
    // ----------------------------------------------------------------------
    //      - Находится в Resources/Project Context.prefab
    //      - Игра корректно работает при запуске из любой сцены.
    // ----------------------------------------------------------------------
    
    public class GameBootstrap : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(DiContainer diContainer, IAssetsProvider assetsProvider)
        {
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private const int TargetFrameRate = 60;
        
        private DiContainer _diContainer;
        private IAssetsProvider _assetsProvider;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            SetApplicationFrameRate();
            CreateScenesLoader();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private static void SetApplicationFrameRate() =>
            Application.targetFrameRate = TargetFrameRate;

        private void CreateScenesLoader()
        {
            GameObject scenesLoaderPrefab = _assetsProvider.GetScenesLoaderPrefab();
            _diContainer.InstantiatePrefab(scenesLoaderPrefab);
        }
    }
}