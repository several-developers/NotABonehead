#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UbicaEditor
{
    public class UbicaToolkitEditor
    {
        // FIELDS: --------------------------------------------------------------------------------

        private const string UbicaEditorMenuItem = "🕹 Not A Bonehead/⚙ Ubica Editor";
        private const string ScenesMenuItem = "🕹 Not A Bonehead/💾 Scenes/";
        private const string ScenesPath = "Assets/Game Core/Scenes/";

        private const string BootstrapSceneMenuItem = ScenesMenuItem + "🚀 Bootstrap";
        private const string LoginSceneMenuItem = ScenesMenuItem + "🗝 Login";
        private const string TitleSceneMenuItem = ScenesMenuItem + "✨ Title";
        private const string MainMenuSceneMenuItem = ScenesMenuItem + "🌐 Main Menu";
        private const string BattleSceneMenuItem = ScenesMenuItem + "⚔ Battle";
        private const string PrototypesSceneMenuItem = ScenesMenuItem + "⏳ Prototypes";

        private const string BootstrapScenePath = ScenesPath + "Bootstrap.unity";
        private const string MainMenuScenePath = ScenesPath + "MainMenu.unity";
        private const string BattleScenePath = ScenesPath + "Battle.unity";

        // PRIVATE METHODS: -----------------------------------------------------------------------

        [MenuItem(BootstrapSceneMenuItem)]
        private static void LoadBootstrapScene() =>
            OpenScene(BootstrapScenePath);

        [MenuItem(MainMenuSceneMenuItem)]
        private static void LoadMainMenuScene() =>
            OpenScene(MainMenuScenePath);

        [MenuItem(BattleSceneMenuItem)]
        private static void LoadBattleScene() =>
            OpenScene(BattleScenePath);

        private static void OpenScene(string path)
        {
            if (!Application.isPlaying && EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        }
    }
}
#endif