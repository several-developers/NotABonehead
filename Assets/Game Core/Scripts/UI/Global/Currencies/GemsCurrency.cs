using GameCore.Events;
using GameCore.Infrastructure.Services.Global.Data;
using Zenject;

namespace GameCore.UI.Global.Currency
{
    public class GemsCurrency : BaseCurrency
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerDataService playerDataService) =>
            _playerDataService = playerDataService;

        // FIELDS: --------------------------------------------------------------------------------

        private const int GemsReward = 5;
        
        private IPlayerDataService _playerDataService;
        
        // PROTECTED METHODS: ---------------------------------------------------------------------
        
        protected override void UpdateValue()
        {
            if (!gameObject.activeSelf)
                return;
            
            int playerGems = GetGems();

            StopValueUpdater();
            StartValueUpdater(playerGems);
        }

        protected override void UpdateValueInstant()
        {
            LastCurrency = GetGems();
           
            UpdateValueText((int)LastCurrency);
        }

        protected override void ClickLogic()
        {
            _playerDataService.AddGems(GemsReward);
            GlobalEvents.SendCurrencyChanged();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private int GetGems() =>
            PlayerDataService.GetGems();
    }
}