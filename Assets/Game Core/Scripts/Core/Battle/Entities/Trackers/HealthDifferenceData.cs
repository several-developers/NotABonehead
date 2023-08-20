namespace GameCore.Battle.Entities
{
    public struct HealthDifferenceData
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public HealthDifferenceData(float health, float maxHealth)
        {
            Health = health;
            MaxHealth = maxHealth;
        }

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public float Health { get; }
        public float MaxHealth { get; }
    }
}