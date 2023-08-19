using GameCore.Battle.Entities;
using GameCore.Enums;

namespace GameCore.Infrastructure.Services.Global.Player
{
    public interface IPlayerStatsCalculatorService
    {
        EntityStats GetPlayerStats();
        float GetPlayerStatValue(StatType statType);
    }
}