using System;
using System.Collections;
using GameCore.Battle.Player;
using GameCore.Battle.Monsters;
using GameCore.Configs;
using GameCore.Infrastructure.Providers.BattleScene.BattleAssets;
using UnityEngine;

namespace GameCore.Battle
{
    public class BattleStateController : IBattleStateController, IDisposable
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public BattleStateController(ICoroutineRunner coroutineRunner, IBattleAssetsProvider battleAssetsProvider,
            IMonsterTracker monsterTracker, IPlayerTracker playerTracker)
        {
            _coroutineRunner = coroutineRunner;
            _monsterTracker = monsterTracker;
            _playerTracker = playerTracker;
            _battleStageConfig = battleAssetsProvider.GetBattleStageConfigMeta();

            _monsterTracker.OnDiedEvent += OnMonsterDied;
            _playerTracker.OnDiedEvent += OnPlayerDied;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IMonsterTracker _monsterTracker;
        private readonly IPlayerTracker _playerTracker;
        private readonly BattleStageConfigMeta _battleStageConfig;

        private Coroutine _battleCO;
        private bool _isPlayerAttackTurn = true;
        private bool _isGameOver;

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
        }

        public void Dispose()
        {
            _monsterTracker.OnDiedEvent -= OnMonsterDied;
            _playerTracker.OnDiedEvent -= OnPlayerDied;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

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
                    _playerTracker.TakeDamage(damage);
                }

                _isPlayerAttackTurn = !_isPlayerAttackTurn;
            }
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnMonsterDied()
        {
            FinishBattle();
        }
        
        private void OnPlayerDied()
        {
            FinishBattle();
        }
    }
}