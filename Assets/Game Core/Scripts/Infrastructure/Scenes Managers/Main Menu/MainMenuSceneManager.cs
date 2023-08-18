using UnityEngine;

namespace GameCore.Infrastructure.ScenesManagers.MainMenu
{
    public class MainMenuSceneManager : MonoBehaviour
    {
        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            Sounds.PlayMainMenuMusic();
    }
}