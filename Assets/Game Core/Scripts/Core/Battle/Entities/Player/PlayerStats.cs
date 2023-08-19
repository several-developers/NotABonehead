namespace GameCore.Battle.Player
{
    public struct PlayerStats
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public PlayerStats(float currentHealth, float maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public float CurrentHealth { get; }
        public float MaxHealth { get; }
    }
}