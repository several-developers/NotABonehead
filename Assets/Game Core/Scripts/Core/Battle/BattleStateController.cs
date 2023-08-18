using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using GameCore.Battle.Monsters;
using GameCore.Battle.Player;
using GameCore.Configs;
using GameCore.Factories;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.UI.BattleScene.GameOverMenus;
using UnityEngine;

namespace GameCore.Battle
{
    public class BattleStateController : IBattleStateController, IDisposable
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public BattleStateController(ICoroutineRunner coroutineRunner, IConfigsProvider configsProvider,
            IMonsterTracker monsterTracker, IPlayerTracker playerTracker, IMonstersDataService monstersDataService,
            IGameDataService gameDataService)
        {
            _coroutineRunner = coroutineRunner;
            _monsterTracker = monsterTracker;
            _playerTracker = playerTracker;
            _monstersDataService = monstersDataService;
            _gameDataService = gameDataService;
            _battleStageConfig = configsProvider.GetBattleStageConfig();

            _monsterTracker.OnDiedEvent += OnMonsterDied;
            _playerTracker.OnDiedEvent += OnPlayerDied;
        }

        // FIELDS: --------------------------------------------------------------------------------

        public event Action<int> OnRoundChangedEvent;

        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IMonsterTracker _monsterTracker;
        private readonly IPlayerTracker _playerTracker;
        private readonly IMonstersDataService _monstersDataService;
        private readonly IGameDataService _gameDataService;
        private readonly BattleStageConfigMeta _battleStageConfig;

        private Coroutine _battleCO;
        private int _currentRound;
        private bool _isPlayerAttackTurn = true;
        private bool _isGameOver;
        private bool _isPlayerWon;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void StartBattle()
        {
            _battleCO = _coroutineRunner.StartCoroutine(BattleCO());
        }

        public void FinishBattle()
        {
            if (_isGameOver)
                return;

            _isGameOver = true;

            StopBattleCO();
            CreateGameOverMenu();
            Sounds.StopMusic();

            if (_isPlayerWon)
            {
                IncreaseMonsterNumber();
                IncreaseLevel();
            }
        }

        public void Dispose()
        {
            _monsterTracker.OnDiedEvent -= OnMonsterDied;
            _playerTracker.OnDiedEvent -= OnPlayerDied;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void CreateGameOverMenu()
        {
            if (_isPlayerWon)
                Sounds.PlaySound(SoundType.Victory);
            else
                Sounds.PlaySound(SoundType.Defeat);
            
            await UniTask.Delay(750);

            if (_isPlayerWon)
                MenuFactory.Create<VictoryMenuView>();
            else
                MenuFactory.Create<DefeatMenuView>();
        }

        private void IncreaseMonsterNumber() =>
            _monstersDataService.IncreaseCurrentMonster();

        private void IncreaseLevel() =>
            _gameDataService.IncreaseCurrentLevel();

        private void StopBattleCO()
        {
            if (_battleCO != null)
                _coroutineRunner.StopCoroutine(BattleCO());
        }

        private IEnumerator BattleCO()
        {
            while (true)
            {
                float attackDelay = _battleStageConfig.AttackDelay;

                yield return new WaitForSeconds(attackDelay);

                if (_isGameOver)
                    yield break;

                if (_isPlayerAttackTurn)
                {
                    int damage = _playerTracker.GetDamage();
                    _playerTracker.SendAttack();
                    _monsterTracker.TakeDamage(damage);
                }
                else
                {
                    int damage = _monsterTracker.GetDamage();
                    _monsterTracker.SendAttack();

                    int playerDefense = _playerTracker.GetDefense();
                    int finalDamage = Mathf.Max(damage - playerDefense, 0);

                    _playerTracker.TakeDamage(finalDamage);
                }
                
                Sounds.PlaySound(SoundType.Hit);

                _isPlayerAttackTurn = !_isPlayerAttackTurn;

                if (_isPlayerAttackTurn)
                    continue;

                _currentRound++;
                OnRoundChangedEvent?.Invoke(_currentRound);
            }
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnMonsterDied()
        {
            _isPlayerWon = true;
            FinishBattle();
        }

        private void OnPlayerDied()
        {
            _isPlayerWon = false;
            FinishBattle();
        }
    }
}