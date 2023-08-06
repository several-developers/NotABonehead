namespace GameCore.Infrastructure.Services.Global.Data
{
    public interface IPlayerDataService
    {
        void AddGold(long gold, bool autoSave = true);
        void AddGems(int gems, bool autoSave = true);
        void SetGold(long gold, bool autoSave = true);
        void SetGems(int gems, bool autoSave = true);
        void SetLevel(int level, bool autoSave = true);
        void SetExperience(int experience, bool autoSave = true);
        long GetGold();
        int GetGems();
        int GetLevel();
        int GetExperience();
    }
}