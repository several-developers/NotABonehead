using System;
using System.Collections.Generic;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Providers.Global.Data;
using GameCore.Battle.Monsters;

namespace GameCore.Infrastructure.Services.Global.Data
{
    public class MonstersDataService : IMonstersDataService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public MonstersDataService(ISaveLoadService saveLoadService, IDataProvider dataProvider,
            IAssetsProvider assetsProvider)
        {
            _saveLoadService = saveLoadService;
            _assetsProvider = assetsProvider;
            _monstersData = dataProvider.GetMonstersData();

            CheckMonstersQueue();
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ISaveLoadService _saveLoadService;
        private readonly IAssetsProvider _assetsProvider;
        private readonly MonstersData _monstersData;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void IncreaseCurrentMonster(bool autoSave)
        {
            int totalMonstersAmount = _monstersData.GetMonstersAmount();
            int currentMonster = _monstersData.CurrentMonster;
            currentMonster++;
            
            bool isOutOfBounce = currentMonster >= totalMonstersAmount;
            
            if (isOutOfBounce)
                ResetMonstersQueue(autoSave: false);
            
            SaveLocalData(autoSave);
        }
        
        public int GetCurrentMonsterIndex() =>
            _monstersData.CurrentMonster;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CheckMonstersQueue()
        {
            int monstersAmountInData = _monstersData.GetMonstersAmount();

            if (monstersAmountInData > 0)
                return;

            ResetMonstersQueue();
        }

        private void ResetMonstersQueue(bool autoSave = true)
        {
            MonsterMeta[] allMonstersMeta = _assetsProvider.GetAllMonstersMeta();
            int monstersAmount = allMonstersMeta.Length;
            List<int> monstersQueue = new(capacity: monstersAmount);

            for (int i = 0; i < monstersAmount; i++)
                monstersQueue.Add(i);

            monstersQueue = ShuffleIntList(monstersQueue);
            _monstersData.SetMonstersQueue(monstersQueue);
            _monstersData.SetCurrentMonster(0);

            SaveLocalData(autoSave);
        }

        private void SaveLocalData(bool autoSave = true)
        {
            if (!autoSave)
                return;

            _saveLoadService.Save();
        }
        
        private static List<int> ShuffleIntList(IList<int> list)
        {
            var random = new Random();
            var newShuffledList = new List<int>();
            int listCount = list.Count;
            
            for (int i = 0; i < listCount; i++)
            {
                var randomElementInList = random.Next(0, list.Count);
                newShuffledList.Add(list[randomElementInList]);
                list.Remove(list[randomElementInList]);
            }
            
            return newShuffledList;
        }
    }
}