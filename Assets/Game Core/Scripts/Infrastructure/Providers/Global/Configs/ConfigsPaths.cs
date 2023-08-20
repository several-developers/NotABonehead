namespace GameCore.Infrastructure.Providers.Global
{
    public static class ConfigsPaths
    {
        // FIELDS: --------------------------------------------------------------------------------
        
        public const string ItemsRarityConfig = Configs + "Items Rarity Config";
        public const string ItemsDropChancesConfig = Configs + "Items Drop Chances Config";
        public const string BattleStageConfig = Configs + "Battle Stage Config";
        public const string PlayerConfig = Configs + "Player Config";
        public const string MonstersConfig = Configs + "Monsters Config";
        public const string GameConfig = Configs + "Game Config";
        public const string ItemsRewardConfig = Configs + "Items Reward Config";
        
        private const string GameData = "Game Data/";
        private const string Configs = GameData + "Configs/";
    }
}