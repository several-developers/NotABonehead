using GameCore.Configs;

namespace GameCore.Infrastructure.Providers.Global
{
    public interface IConfigsProvider
    {
        ItemsRarityConfigMeta GetItemsRarityConfig();
        ItemsDropChancesConfigMeta GetItemsDropChancesConfig();
        BattleStageConfigMeta GetBattleStageConfig();
        PlayerConfigMeta GetPlayerConfig();
        MonstersConfigMeta GetMonstersConfig();
    }
}