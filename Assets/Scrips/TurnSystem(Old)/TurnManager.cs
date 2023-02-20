using System;
using System.Collections.Generic;

namespace Scrips.TurnSystem
{
    public class TurnManager
    {
        public event Action<PlayerTurn> OnStartNewTurn; 

        private readonly List<PlayerTurn> _allPlayers;
        private readonly Queue<Team> _teamQueue;

        private PlayerTurn _currentPlayerTurn;

        public PlayerTurn CurrentPlayerTurn => _currentPlayerTurn;

        public TurnManager(TurnManagerConfig config)
        {
            _allPlayers = config.AllPlayers;
            
            if (_allPlayers.Count == 0)
                throw new Exception("Turn Manager not Initialized");

            _teamQueue = new Queue<Team>();

            foreach (var team in config.Teams)
            {
                team.InitQueue();
                _teamQueue.Enqueue(team);
            }
        }
        
        public void NextTurnTurns()
        {
            _currentPlayerTurn?.ExitTurn();

            Team cache = _teamQueue.Dequeue();
            _currentPlayerTurn = cache.GetNextPlayerInTeam();
            
            _currentPlayerTurn.EnterTurn();
            OnStartNewTurn?.Invoke(_currentPlayerTurn);
            
            _teamQueue.Enqueue(cache);
        }
    }
}