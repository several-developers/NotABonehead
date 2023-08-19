namespace GameCore.Battle
{
    public class GameOverHandler : IGameOverHandler
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public GameOverHandler(IVictoryHandler victoryHandler, IDefeatHandler defeatHandler)
        {
            _victoryHandler = victoryHandler;
            _defeatHandler = defeatHandler;
        }

        // FIELDS: --------------------------------------------------------------------------------
        
        private readonly IVictoryHandler _victoryHandler;
        private readonly IDefeatHandler _defeatHandler;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void HandleGameOver(bool isPlayerWon)
        {
            if (isPlayerWon)
                _victoryHandler.HandleVictory();
            else
                _defeatHandler.HandleDefeat();
        }
    }
}