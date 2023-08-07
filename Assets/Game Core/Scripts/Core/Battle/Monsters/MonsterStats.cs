namespace GameCore.Battle.Monsters
{
    public struct MonsterStats
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public MonsterStats(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public int CurrentHealth { get; }
        public int MaxHealth { get; }
    }
}