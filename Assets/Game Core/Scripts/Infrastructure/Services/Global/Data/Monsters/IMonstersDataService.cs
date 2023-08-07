namespace GameCore.Infrastructure.Services.Global.Data
{
    public interface IMonstersDataService
    {
        void IncreaseCurrentMonster(bool autoSave = true);
        int GetCurrentMonsterIndex();
    }
}