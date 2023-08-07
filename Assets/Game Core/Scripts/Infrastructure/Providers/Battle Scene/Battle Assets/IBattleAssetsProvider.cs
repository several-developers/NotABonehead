using GameCore.Configs;

namespace GameCore.Infrastructure.Providers.BattleScene.BattleAssets
{
    public interface IBattleAssetsProvider
    {
        BattleStageConfigMeta GetBattleStageConfigMeta();
    }
}