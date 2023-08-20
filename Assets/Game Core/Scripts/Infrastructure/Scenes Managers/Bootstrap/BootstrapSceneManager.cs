using GameCore.Enums;
using GameCore.Infrastructure.Services.Global;
using UnityEngine;
using Zenject;

namespace GameCore.Infrastructure.ScenesManagers.BootstrapScene
{
    public class BootstrapSceneManager : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IScenesLoaderService scenesLoaderService) =>
            _scenesLoaderService = scenesLoaderService;

        // FIELDS: --------------------------------------------------------------------------------

        private IScenesLoaderService _scenesLoaderService;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start()
        {
            // Load some SDK or assets

            _scenesLoaderService.LoadScene(SceneName.MainMenu);
        }
    }
}