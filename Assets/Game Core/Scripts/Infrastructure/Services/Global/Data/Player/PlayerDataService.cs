using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global.Data;

namespace GameCore.Infrastructure.Services.Global.Data
{
    public class PlayerDataService : IPlayerDataService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public PlayerDataService(ISaveLoadService saveLoadService, IDataProvider dataProvider)
        {
            _saveLoadService = saveLoadService;
            _playerData = dataProvider.GetPlayerData();
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ISaveLoadService _saveLoadService;
        private readonly PlayerData _playerData;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void SetGold(long gold, bool autoSave = true)
        {
            _playerData.SetGold(gold);
            SaveLocalData(autoSave);
        }

        public void SetGems(int crystals, bool autoSave = true)
        {
            _playerData.SetGems(crystals);
            SaveLocalData(autoSave);
        }

        public void SetLevel(int level, bool autoSave = true)
        {
            _playerData.SetLevel(level);
            SaveLocalData(autoSave);
        }

        public void SetExperience(int experience, bool autoSave = true)
        {
            _playerData.SetExperience(experience);
            SaveLocalData(autoSave);
        }

        public long GetGold() =>
            _playerData.Gold;

        public int GetGems() =>
            _playerData.Gems;

        public int GetLevel() =>
            _playerData.Level;

        public int GetExperience() =>
            _playerData.Experience;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SaveLocalData(bool autoSave = true)
        {
            if (!autoSave)
                return;
            
            _saveLoadService.Save();
        }
    }
}