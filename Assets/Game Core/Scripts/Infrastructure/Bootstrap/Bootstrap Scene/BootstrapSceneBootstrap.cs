using GameCore.Enums;
using GameCore.Other;
using UnityEngine;
using Zenject;

namespace GameCore.Infrastructure.Bootstrap.BootstrapScene
{
    public class BootstrapSceneBootstrap : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IScenesLoader scenesLoader) =>
            _scenesLoader = scenesLoader;

        // FIELDS: --------------------------------------------------------------------------------
        
        private IScenesLoader _scenesLoader;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            _scenesLoader.LoadScene(SceneName.MainMenu);
    }
}