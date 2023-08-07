namespace GameCore
{
    public static class Sounds
    {
        // FIELDS: --------------------------------------------------------------------------------

        private static SoundManager _soundManager;
        private static bool _isInitialized;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void Setup(SoundManager soundManager)
        {
            if (_isInitialized)
                return;
            
            _soundManager = soundManager;
            _isInitialized = true;
        }

        public static void PlayMainMenuMusic() =>
            _soundManager.PlayMainMenuMusic();

        public static void PlayBattleMusic() =>
            _soundManager.PlayBattleMusic();

        public static void StopMusic() =>
            _soundManager.StopMusic();

        public static void PlaySound(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.Click1: _soundManager.PlayClick1(); break;
                case SoundType.Click2: _soundManager.PlayClick2(); break;
                case SoundType.Hit: _soundManager.PlayHit(); break;
                case SoundType.Victory: _soundManager.PlayVictory(); break;
                case SoundType.Defeat: _soundManager.PlayDefeat(); break;
                case SoundType.Collected: _soundManager.PlayCollected(); break;
                case SoundType.LvlUp: _soundManager.PlayLvlUp(); break;
            }
        }
    }
}