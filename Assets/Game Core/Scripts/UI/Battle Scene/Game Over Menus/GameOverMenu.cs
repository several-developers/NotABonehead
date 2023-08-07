using GameCore.Enums;
using GameCore.Other;
using GameCore.UI.Global.MenuView;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI.BattleScene.GameOverMenus
{
    public abstract class GameOverMenu : MenuView
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IScenesLoader scenesLoader) =>
            _scenesLoader = scenesLoader;

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _continueButton;

        // FIELDS: --------------------------------------------------------------------------------
        
        private IScenesLoader _scenesLoader;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            _continueButton.onClick.AddListener(OnContinueClicked);
            
            DestroyOnHide();
        }

        private void Start() => Show();

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnContinueClicked() =>
            _scenesLoader.LoadScene(SceneName.MainMenu);
    }
}
