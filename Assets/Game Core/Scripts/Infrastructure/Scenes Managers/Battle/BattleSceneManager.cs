using GameCore.Battle;
using GameCore.Battle.Entities.Monsters;
using GameCore.Battle.Entities.Player;
using UnityEngine;
using Zenject;

namespace GameCore.Infrastructure.Bootstrap.BattleScene
{
    public class BattleSceneManager : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IMonstersFactory monstersFactory, IPlayerFactory playerFactory,
            IBattleStateController battleStateController)
        {
            _monstersFactory = monstersFactory;
            _playerFactory = playerFactory;
            _battleStateController = battleStateController;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private IMonstersFactory _monstersFactory;
        private IPlayerFactory _playerFactory;
        private IBattleStateController _battleStateController;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() => Init();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Init()
        {
            PlayBattleMusic();
            CreateMonster();
            CreatePlayer();
            StartBattle();
        }

        private void CreateMonster() =>
            _monstersFactory.Create();

        private void CreatePlayer() =>
            _playerFactory.Create();

        private void StartBattle() =>
            _battleStateController.StartBattle();

        private static void PlayBattleMusic() =>
            Sounds.PlayBattleMusic();
    }
}