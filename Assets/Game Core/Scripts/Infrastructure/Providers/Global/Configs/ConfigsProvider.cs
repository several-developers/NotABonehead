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
            _battleStageConfig = Load<BattleStageConfigMeta>(path: ConfigsPaths.BattleStageConfig);
            _playerConfig = Load<PlayerConfigMeta>(path: ConfigsPaths.PlayerConfig);
            _monstersConfig = Load<MonstersConfigMeta>(path: ConfigsPaths.MonstersConfig);
            _gameConfig = Load<GameConfigMeta>(path: ConfigsPaths.GameConfig);
            _itemsRewardConfig = Load<ItemsRewardConfigMeta>(path: ConfigsPaths.ItemsRewardConfig);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ItemsRarityConfigMeta _itemsRarityConfig;
        private readonly ItemsDropChancesConfigMeta _itemsDropChancesConfig;
        private readonly BattleStageConfigMeta _battleStageConfig;
        private readonly PlayerConfigMeta _playerConfig;
        private readonly MonstersConfigMeta _monstersConfig;
        private readonly GameConfigMeta _gameConfig;
        private readonly ItemsRewardConfigMeta _itemsRewardConfig;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public ItemsRarityConfigMeta GetItemsRarityConfig() => _itemsRarityConfig;
        public ItemsDropChancesConfigMeta GetItemsDropChancesConfig() => _itemsDropChancesConfig;
        public BattleStageConfigMeta GetBattleStageConfig() => _battleStageConfig;
        public PlayerConfigMeta GetPlayerConfig() => _playerConfig;
        public MonstersConfigMeta GetMonstersConfig() => _monstersConfig;
        public GameConfigMeta GetGameConfig() => _gameConfig;
        public ItemsRewardConfigMeta GetItemsRewardConfig() => _itemsRewardConfig;
    }
}