using GameCore.Configs;
using GameCore.Utilities;

namespace GameCore.Infrastructure.Providers.Global
{
    public class ConfigsProvider : AssetsProviderBase, IConfigsProvider
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ConfigsProvider()
        {
            _itemsRarityConfig = Load<ItemsRarityConfigMeta>(path: ConfigsPaths.ItemsRarityConfig);
            _itemsDropChancesConfig = Load<ItemsDropChancesConfigMeta>(path: ConfigsPaths.ItemsDropChancesConfig);
            _battleStageConfigMeta = Load<BattleStageConfigMeta>(path: ConfigsPaths.BattleStageConfigMeta);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ItemsRarityConfigMeta _itemsRarityConfig;
        private readonly ItemsDropChancesConfigMeta _itemsDropChancesConfig;
        private readonly BattleStageConfigMeta _battleStageConfigMeta;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public ItemsRarityConfigMeta GetItemsRarityConfig() => _itemsRarityConfig;
        public ItemsDropChancesConfigMeta GetItemsDropChancesConfig() => _itemsDropChancesConfig;
        public BattleStageConfigMeta GetBattleStageConfig() => _battleStageConfigMeta;
    }
}