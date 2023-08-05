namespace GameCore.UI.Global.Currency
{
    public class GemsCurrency : BaseCurrency
    {
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

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private int GetGems() =>
            PlayerDataService.GetGems();
    }
}