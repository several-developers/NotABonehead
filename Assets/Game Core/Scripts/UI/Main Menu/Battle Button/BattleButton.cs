using GameCore.Enums;
using GameCore.Other;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI.MainMenu
{
    public class BattleButton : MonoBehaviour
    {
        [Inject]
        private void Construct(IScenesLoader scenesLoader) =>
            _scenesLoader = scenesLoader;

        public Button button;
        
        private IScenesLoader _scenesLoader;

        private void Awake() =>
            button.onClick.AddListener(OnClick);

        private void OnClick() =>
            _scenesLoader.LoadScene(SceneName.Battle);
    }
}
