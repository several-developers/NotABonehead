using GameCore.Infrastructure.Data;

namespace GameCore.Infrastructure.Providers.Global.Data
{
    public interface IDataProvider
    {
        DataManager GetDataManager();
        GameData GetGameData();
        PlayerData GetPlayerData();
        GameSettingsData GetGameSettingsData();
        InventoryData GetInventoryData();
    }
}