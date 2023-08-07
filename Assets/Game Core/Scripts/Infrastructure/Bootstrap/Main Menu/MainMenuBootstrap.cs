using UnityEngine;

namespace GameCore.Infrastructure.Bootstrap.MainMenu
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            Sounds.PlayMainMenuMusic();
    }
}