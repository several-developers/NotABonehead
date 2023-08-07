namespace GameCore.Infrastructure.Providers.Global
{
    public static class AssetsPaths
    {
        // FIELDS: --------------------------------------------------------------------------------
        
        public const string Items = GameData + "Items/";
        public const string MenuPrefabsListMeta = Settings + "Menu Prefabs List";
        public const string GameItemPrefabsListMeta = Settings + "Game Item Prefabs List";
        public const string ItemsRarityConfigMeta = Settings + "Items Rarity Config";
        public const string ItemsDropChancesConfigMeta = Settings + "Items Drop Chances Config";
        public const string MonstersMeta = GameData + "Monsters";

        private const string GameData = "Game Data/";
        private const string Settings = GameData + "Settings/";
    }
}