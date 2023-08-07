using System;
using GameCore.Infrastructure.Data;

namespace GameCore.Infrastructure.Providers.Global.Data
{
    public class DataProvider : IDataProvider, IDisposable
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public DataProvider()
        {
            _dataManager = new DataManager();
            _dataManager.LoadLocalData();
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly DataManager _dataManager;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public DataManager GetDataManager() => _dataManager;

        public GameData GetGameData() =>
            _dataManager.GameData;

        public PlayerData GetPlayerData() =>
            _dataManager.PlayerData;

        public GameSettingsData GetGameSettingsData() =>
            _dataManager.GameSettingsData;

        public InventoryData GetInventoryData() =>
            _dataManager.InventoryData;
        
        public MonstersData GetMonstersData() =>
            _dataManager.MonstersData;

        public void Dispose() =>
            _dataManager.SaveLocalData();
    }
}