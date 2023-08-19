using Cysharp.Threading.Tasks;
using GameCore.Events;
using GameCore.Infrastructure.Services.Global.Data;
using UnityEngine;
using Zenject;

namespace GameCore.UI.Global.Currency
{
    public class GoldCurrency : BaseCurrency
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerDataService playerDataService) =>
            _playerDataService = playerDataService;

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Min(0)]
        private float _updateValueDelay;
        
        // FIELDS: --------------------------------------------------------------------------------

        private const long GoldReward = 25;
        
        private IPlayerDataService _playerDataService;
        
        // PROTECTED METHODS: ---------------------------------------------------------------------
        
        protected override async void UpdateValue()
        {
            if (!gameObject.activeSelf)
                return;
            
            long playerMoney = GetGold();

            int delay = (int)(_updateValueDelay * 1000);
            bool isCanceled = await UniTask
                .Delay(delay, cancellationToken: this.GetCancellationTokenOnDestroy())
                .SuppressCancellationThrow();

            if (isCanceled)
                return;
            
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