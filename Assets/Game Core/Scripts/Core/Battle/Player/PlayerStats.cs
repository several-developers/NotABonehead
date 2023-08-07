namespace GameCore.Battle.Player
{
    public struct PlayerStats
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public PlayerStats(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public int CurrentHealth { get; }
        public int MaxHealth { get; }
    }
}