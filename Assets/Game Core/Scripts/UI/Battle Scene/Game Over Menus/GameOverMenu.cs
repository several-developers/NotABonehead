using GameCore.Enums;
using GameCore.Infrastructure.Services.Global;
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
        private void Construct(IScenesLoaderService scenesLoaderService) =>
            _scenesLoaderService = scenesLoaderService;

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _continueButton;

        // FIELDS: --------------------------------------------------------------------------------
        
        private IScenesLoaderService _scenesLoaderService;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            _continueButton.onClick.AddListener(OnContinueClicked);
            
            DestroyOnHide();
        }

        private void Start() => Show();

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnContinueClicked() =>
            _scenesLoaderService.LoadScene(SceneName.MainMenu);
    }
}
