﻿using System;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace GameCore.Infrastructure.Data
{
    [Serializable]
    public class DataManager
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public DataManager()
        {
            _gameData = new GameData();
            _gameSettingsData = new GameSettingsData();
            _playerData = new PlayerData();
            _inventoryData = new InventoryData();
            _monstersData = new MonstersData();
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup(Constants.Settings)]
        [BoxGroup(EditorConstants.GameData, showLabel: false), SerializeField]
        private GameData _gameData;

        [BoxGroup(EditorConstants.PlayerData, showLabel: false), SerializeField]
        private PlayerData _playerData;

        [BoxGroup(EditorConstants.GameSettings, showLabel: false), SerializeField]
        private GameSettingsData _gameSettingsData;

        [BoxGroup(EditorConstants.InventoryData, showLabel: false), SerializeField]
        private InventoryData _inventoryData;
        
        [BoxGroup(EditorConstants.MonstersData, showLabel: false), SerializeField]
        private MonstersData _monstersData;

        // PROPERTIES: ----------------------------------------------------------------------------

        public GameData GameData => _gameData ??= new();
        public PlayerData PlayerData => _playerData ??= new();
        public GameSettingsData GameSettingsData => _gameSettingsData ??= new();
        public InventoryData InventoryData => _inventoryData ??= new();
        public MonstersData MonstersData => _monstersData ??= new();

        // FIELDS: --------------------------------------------------------------------------------
        
        private const string ResetLocalDataTitle = "Reset Local Data";
        private const string LoadLocalDataTitle = "Load Local Data";
        private const string SaveLocalDataTitle = "Save Local Data";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void LoadLocalData()
        {
            TryLoadData(ref _gameData);
            TryLoadData(ref _playerData);
            TryLoadData(ref _gameSettingsData);
            TryLoadData(ref _inventoryData);
            TryLoadData(ref _monstersData);
        }

        public void SaveLocalData()
        {
            TrySaveData(_gameData);
            TrySaveData(_playerData);
            TrySaveData(_gameSettingsData);
            TrySaveData(_inventoryData);
            TrySaveData(_monstersData);
        }

        public void SaveLocalData<T>(T t) where T : DataBase => TrySaveData(t);

        public void DeleteLocalData()
        {
            TryDeleteData(ref _gameData);
            TryDeleteData(ref _playerData);
            TryDeleteData(ref _gameSettingsData);
            TryDeleteData(ref _inventoryData);
            TryDeleteData(ref _monstersData);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void TrySetData<T>(string data, ref T t) where T : DataBase
        {
            if (data.IsNullOrWhitespace())
                return;

            t ??= (T)Activator.CreateInstance(typeof(T));
            t = JsonUtility.FromJson<T>(data);
        }

        private void TryLoadData<T>(ref T t) where T : DataBase
        {
            string path = t.GetDataPath();
            bool isFileExists = File.Exists(path);

            if (!isFileExists)
            {
                t = (T)Activator.CreateInstance(typeof(T));
                string json = JsonUtility.ToJson(t);
                File.WriteAllText(path, json);
            }

            string data = File.ReadAllText(path);
            TrySetData(data, ref t);
        }

        private void TrySaveData<T>(T t) where T : DataBase
        {
            t ??= (T)Activator.CreateInstance(typeof(T));

            string path = t.GetDataPath();
            string data = JsonUtility.ToJson(t);
            
            File.WriteAllText(path, data);
        }

        private void TryDeleteData<T>(ref T t) where T : DataBase
        {
            t ??= (T)Activator.CreateInstance(typeof(T));

            string path = t.GetDataPath();
            
            if (File.Exists(path))
                File.Delete(path);

            t = (T)Activator.CreateInstance(typeof(T));
        }

        // DEBUG BUTTONS: -------------------------------------------------------------------------

        [Title(Constants.DebugButtons)]
        [Button(ButtonHeight = 35), GUIColor(0.4f, 1f, 0.4f)]
        [LabelText(SaveLocalDataTitle)]
        private void DebugSaveLocalData() => SaveLocalData();

        [Button(ButtonHeight = 35), GUIColor(0.5f, 0.5f, 1)]
        [LabelText(LoadLocalDataTitle)]
        private void DebugLoadLocalData() => LoadLocalData();

        [Button(ButtonHeight = 20), GUIColor(1f, 0.4f, 0.4f)]
        [LabelText(ResetLocalDataTitle)]
        private void DebugResetLocalData()
        {
            DeleteLocalData();
            SaveLocalData();
            LoadLocalData();
        }
    }
}