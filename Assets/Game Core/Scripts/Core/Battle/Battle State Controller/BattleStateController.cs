using System;
using GameCore.Battle.Monsters;
using GameCore.Battle.Player;
using GameCore.Infrastructure.Providers.Global;

namespace GameCore.Battle
{
    public class BattleStateController : IBattleStateController, IDisposable
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public BattleStateController(ICoroutineRunner coroutineRunner, IConfigsProvider configsProvider,
            IMonsterTracker monsterTracker, IPlayerTracker playerTracker, IGameOverHandler gameOverHandler)
        {
            _monsterTracker = monsterTracker;
            _playerTracker = playerTracker;
            _gameOverHandler = gameOverHandler;
            _fightSystem = new FightSystem(playerTracker, monsterTracker, coroutineRunner, configsProvider);

            _monsterTracker.OnDiedEvent += OnMonsterDied;
            _playerTracker.OnDiedEvent += OnPlayerDied;
            _fightSystem.OnRoundChangedEvent += OnRoundChanged;
        }

        // FIELDS: --------------------------------------------------------------------------------

        public event Action<int> OnRoundChangedEvent;

        private readonly IMonsterTracker _monsterTracker;
        private readonly IPlayerTracker _playerTracker;
        private readonly IGameOverHandler _gameOverHandler;
        private readonly FightSystem _fightSystem;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void StartBattle() =>
            _fightSystem.StartFight();

        public void FinishBattle(bool isPlayerWon)
        {
            StopMusic();
            _fightSystem.StopFight();
            _gameOverHandler.HandleGameOver(isPlayerWon);
        }

        public void Dispose()
        {
            _monsterTracker.OnDiedEvent -= OnMonsterDied;
            _playerTracker.OnDiedEvent -= OnPlayerDied;
            _fightSystem.OnRoundChangedEvent -= OnRoundChanged;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static void StopMusic() =>
            Sounds.StopMusic();

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnMonsterDied() => FinishBattle(isPlayerWon: true);

        private void OnPlayerDied() => FinishBattle(isPlayerWon: false);

        private void OnRoundChanged(int round) =>
            OnRoundChangedEvent?.Invoke(round);
    }
}