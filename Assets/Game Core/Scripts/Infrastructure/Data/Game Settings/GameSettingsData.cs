using System;
using UnityEngine;

namespace GameCore.Infrastructure.Data
{
    [Serializable]
    public class GameSettingsData : DataBase
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Range(0, 1)]
        private float _soundVolume = 1;

        [SerializeField, Range(0, 1)]
        private float _musicVolume = 1;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string DataKey => Constants.GameSettingsDataKey;
        public float SoundVolume => _soundVolume;
        public float MusicVolume => _musicVolume;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetSoundVolume(float value) =>
            _soundVolume = Mathf.Clamp01(value);

        public void SetMusicVolume(float value) =>
            _musicVolume = Mathf.Clamp01(value);
    }
}