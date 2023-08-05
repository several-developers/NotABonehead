using System;
using GameCore.AllConstants;
using UnityEngine;

namespace GameCore.Infrastructure.Data
{
    [Serializable]
    public class PlayerData : DataBase
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Min(0)]
        private long _gold;

        [SerializeField, Min(0)]
        private int _gems;

        [SerializeField, Min(1)]
        private int _level = 1;

        [SerializeField, Min(0)]
        private int _experience;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string DataKey => Constants.PlayerDataKey;
        public long Gold => _gold;
        public int Gems => _gems;
        public int Level => _level;
        public int Experience => _experience;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void AddGold(long amount) =>
            _gold += amount;

        public void AddGems(int amount) =>
            _gems += amount;

        public void AddLevel(int amount) =>
            _level += amount;

        public void AddExperience(int amount) =>
            _experience += amount;

        public void SetGold(long amount) =>
            _gold = Math.Max(amount, 0);

        public void SetGems(int amount) =>
            _gems = Mathf.Max(amount, 0);

        public void SetLevel(int amount) =>
            _level = Mathf.Max(amount, 1);

        public void SetExperience(int amount) =>
            _experience = amount;

        public void SpendGold(long amount) =>
            _gold = Math.Max(_gold - amount, 0);

        public void SpendGems(int amount) =>
            _gems = Mathf.Max(_gems - amount, 0);
    }
}