using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scrips.TurnSystem
{
    [Serializable]
    public class Team
    {
        [SerializeField] private string _teamName;
        [SerializeField] private Color _teamColor;

        [SerializeField] private List<PlayerTurn> _teamPlayers;

        private Queue<PlayerTurn> _playerTurns;

        private PlayerTurn _currentPlayerTurn;

        public List<PlayerTurn> TeamPlayers => _teamPlayers;
        public string TeamName => _teamName;
        public Color TeamColor => _teamColor;
        
        public Team(string teamName)
        {
            _teamName = teamName;
        }

        public PlayerTurn GetNextPlayerInTeam()
        {
            _currentPlayerTurn = _playerTurns.Dequeue();
            _playerTurns.Enqueue(_currentPlayerTurn);

            return _currentPlayerTurn;
        }

        public void InitQueue()
        {
            _playerTurns = new Queue<PlayerTurn>(_teamPlayers.Count);

            foreach (var player in _teamPlayers)
            {
                _playerTurns.Enqueue(player);
            }
        }

        [Button("Add new player")]
        private void AddNewPlayer()
        {
            var cache = new PlayerTurn("New player", this);
            
            _teamPlayers.Add(cache);
        }
        
    }
}