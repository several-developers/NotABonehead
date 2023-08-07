using GameCore.Configs;
using UnityEngine;

namespace GameCore.Infrastructure.Providers.BattleScene.BattleAssets
{
    public class BattleAssetsProvider : IBattleAssetsProvider
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public BattleAssetsProvider()
        {
            _battleStageConfigMeta = Load<BattleStageConfigMeta>(path: BattleAssetsPaths.BattleStageConfigMeta);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly BattleStageConfigMeta _battleStageConfigMeta;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public BattleStageConfigMeta GetBattleStageConfigMeta() => _battleStageConfigMeta;
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static T Load<T>(string path) where T : Object =>
            Resources.Load<T>(path);

        private static T[] LoadAll<T>(string path) where T : Object =>
            Resources.LoadAll<T>(path);
    }
}