using System;
using System.Collections;
using GameCore.Battle.Monsters;
using GameCore.Battle.Player;
using GameCore.Configs;
using GameCore.Infrastructure.Providers.Global;
using UnityEngine;

namespace GameCore.Battle
{
    public class FightSystem
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public FightSystem(IPlayerTracker playerTracker, IMonsterTracker monsterTracker,
            ICoroutineRunner coroutineRunner, IConfigsProvider configsProvider)
        {
            _playerTracker = playerTracker;
            _monsterTracker = monsterTracker;
            _coroutineRunner = coroutineRunner;
            _battleStageConfig = configsProvider.GetBattleStageConfig();
            _isPlayerTurn = true;
        }

        // FIELDS: --------------------------------------------------------------------------------

        public event Action<int> OnRoundChangedEvent;

        private readonly IPlayerTracker _playerTracker;
        private readonly IMonsterTracker _monsterTracker;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly BattleStageConfigMeta _battleStageConfig;

        private Coroutine _fightCO;
        private int _currentRound;
        private bool _isFightOver;
        private bool _isPlayerTurn;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void StartFight()
        {
            _isFightOver = false;
            _fightCO = _coroutineRunner.StartCoroutine(FightCO());
        }

        public void StopFight()
        {
            if (_fightCO == null)
                return;

            _isFightOver = true;
            _coroutineRunner.StopCoroutine(_fightCO);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private IEnumerator FightCO()
        {
            while (true)
            {
                float attackDelay = _battleStageConfig.AttackDelay;

                yield return new WaitForSeconds(attackDelay);

                if (_isFightOver)
                    yield break;

                if (_isPlayerTurn)
                {
                    float damage = _playerTracker.GetDamage();
                    _playerTracker.SendAttack();
                    _monsterTracker.TakeDamage(damage);
                }
                else
                {
                    float damage = _monsterTracker.GetDamage();
                    _monsterTracker.SendAttack();

                    float playerDefense = _playerTracker.GetDefense();
                    float finalDamage = Mathf.Max(damage - playerDefense, 0);

                    _playerTracker.TakeDamage(finalDamage);
                }

                Sounds.PlaySound(SoundType.Hit);

                _isPlayerTurn = !_isPlayerTurn;

                if (_isPlayerTurn)
                    continue;

                _currentRound++;
                OnRoundChangedEvent?.Invoke(_currentRound);
            }
        }
    }
}