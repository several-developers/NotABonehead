using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Infrastructure.Data
{
    [Serializable]
    public class MonstersData : DataBase
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public MonstersData() =>
            _monstersQueue = new List<int>(capacity: 32);

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private int _currentMonster;
        
        [SerializeField, Space(5)]
        private List<int> _monstersQueue;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string DataKey => Constants.MonstersDataKey;
        public int CurrentMonster => _currentMonster;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetCurrentMonster(int currentMonster) =>
            _currentMonster = currentMonster;

        public void SetMonstersQueue(List<int> monstersQueue) =>
            _monstersQueue = monstersQueue;

        public int GetMonstersAmount() => _monstersQueue.Count;
    }
}