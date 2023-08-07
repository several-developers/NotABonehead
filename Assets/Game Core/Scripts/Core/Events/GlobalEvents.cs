using System;
using GameCore.Enums;

namespace GameCore.Events
{
    public static class GlobalEvents
    {
        // FIELDS: --------------------------------------------------------------------------------

        public static event Action<CurrencyType> OnCurrencyChanged;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void SendCurrencyChanged(CurrencyType currencyType = CurrencyType.Gold) =>
            OnCurrencyChanged?.Invoke(currencyType);
    }
}