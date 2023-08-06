using GameCore.Events;
using GameCore.Infrastructure.Services.Global.Data;
using Zenject;

namespace GameCore.UI.Global.Currency
{
    public class GoldCurrency : BaseCurrency
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerDataService playerDataService) =>
            _playerDataService = playerDataService;

        // FIELDS: --------------------------------------------------------------------------------

        private const long GoldReward = 25;
        
        private IPlayerDataService _playerDataService;
        
        // PROTECTED METHODS: ---------------------------------------------------------------------
        
        protected override void UpdateValue()
        {
            if (!gameObject.activeSelf)
                return;
            
            long playerMoney = GetGold();

            StopValueUpdater();
            StartValueUpdater(playerMoney);
        }

        protected override void UpdateValueInstant()
        {
            LastCurrency = GetGold();
            UpdateValueText((int)LastCurrency);
        }

        protected override void ClickLogic()
        {
            _playerDataService.AddGold(GoldReward);
            GlobalEvents.SendCurrencyChanged();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private long GetGold() =>
            PlayerDataService.GetGold();
    }
}