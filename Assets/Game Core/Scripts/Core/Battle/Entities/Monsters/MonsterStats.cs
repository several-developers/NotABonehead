namespace GameCore.Battle.Entities.Monsters
{
    public struct MonsterStats
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public MonsterStats(float currentHealth, float maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public float CurrentHealth { get; }
        public float MaxHealth { get; }
    }
}