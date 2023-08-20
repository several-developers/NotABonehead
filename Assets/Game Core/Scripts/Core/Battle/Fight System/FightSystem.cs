using System;
using System.Collections;
using GameCore.Battle.Entities;
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
                    PlayerTurn();
                else
                    MonsterTurn();

                PlayHitSound();
                HandleEndOfTheTurn();
            }
        }

        private void HandleEndOfTheTurn()
        {
            _isPlayerTurn = !_isPlayerTurn;

            if (_isPlayerTurn)
                return;

            _currentRound++;
            OnRoundChangedEvent?.Invoke(_currentRound);
        }

        private void PlayerTurn() => HandleTurn(attacker: _playerTracker, defender: _monsterTracker);

        private void MonsterTurn() => HandleTurn(attacker: _monsterTracker, defender: _playerTracker);

        private static void HandleTurn(IEntityTracker attacker, IEntityTracker defender)
        {
            EntityStats attackerStats = attacker.GetEntityStats();
            float damage = attackerStats.Damage;

            EntityStats defenderStats = defender.GetEntityStats();
            float defense = defenderStats.Defense;
            
            float finalDamage = CalculateDamage(damage, defense);
            
            attacker.SendAttackEvent();
            defender.TakeDamage(finalDamage);
        }

        private static float CalculateDamage(float damage, float defense) =>
            Mathf.Max(damage - defense, 0);

        private static void PlayHitSound() =>
            Sounds.PlaySound(SoundType.Hit);
    }
}