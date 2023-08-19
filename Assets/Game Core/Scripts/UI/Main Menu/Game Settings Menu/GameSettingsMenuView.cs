using GameCore.Infrastructure.Services.Global;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.UI.Global.MenuView;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI.MainMenu.GameSettingsMenu
{
    public class GameSettingsMenuView : MenuView
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IGameSettingsDataService gameSettingsDataService, ISaveLoadService saveLoadService)
        {
            _gameSettingsDataService = gameSettingsDataService;
            _saveLoadService = saveLoadService;

            float soundVolume = _gameSettingsDataService.GetSoundVolume();
            float musicVolume = _gameSettingsDataService.GetMusicVolume();

            _soundVolumeSlider.value = soundVolume;
            _musicVolumeSlider.value = musicVolume;
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _closeButton;
        
        [SerializeField, Required]
        private Button _overlayCloseButton;

        [SerializeField, Required]
        private Slider _soundVolumeSlider;
        
        [SerializeField, Required]
        private Slider _musicVolumeSlider;

        // FIELDS: --------------------------------------------------------------------------------

        private IGameSettingsDataService _gameSettingsDataService;
        private ISaveLoadService _saveLoadService;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseClicked);
            _overlayCloseButton.onClick.AddListener(OnCloseClicked);
            _soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeChanged);
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            
            DestroyOnHide();
        }

        private void Start() => Show();

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnCloseClicked()
        {
            Hide();
            _saveLoadService.Save();
        }

        private void OnSoundVolumeChanged(float value)
        {
            value = Mathf.Clamp01(value);
            _gameSettingsDataService.SetSoundVolume(value, autoSave: false);
        }

        private void OnMusicVolumeChanged(float value)
        {
            value = Mathf.Clamp01(value);
            _gameSettingsDataService.SetMusicVolume(value, autoSave: false);
        }
    }
}