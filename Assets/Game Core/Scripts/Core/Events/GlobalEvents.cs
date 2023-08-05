using System;

namespace GameCore.Events
{
    public static class GlobalEvents
    {
        // FIELDS: --------------------------------------------------------------------------------

        public static event Action OnCurrencyChanged;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void SendCurrencyChanged() => OnCurrencyChanged?.Invoke();
    }
}