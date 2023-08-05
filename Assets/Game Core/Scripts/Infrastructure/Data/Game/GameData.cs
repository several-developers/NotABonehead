using System;
using GameCore.AllConstants;
using UnityEngine;

namespace GameCore.Infrastructure.Data
{
    [Serializable]
    public class GameData : DataBase
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Min(1)]
        private int _currentLevel = 1;

        [SerializeField, Min(0)]
        private int _sessionNumber;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string DataKey => Constants.GameDataKey;
        public int CurrentLevel => _currentLevel;
        public int SessionNumber => _sessionNumber;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetCurrentLevel(int level) =>
            _currentLevel = Mathf.Max(level, 1);

        public void IncreaseSessionNumber() =>
            _sessionNumber += 1;
    }
}