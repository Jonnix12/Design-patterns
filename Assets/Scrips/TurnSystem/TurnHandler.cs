using System;
using System.Collections.Generic;
using DesignPatterns.Players;

namespace DesignPatterns.TurnSystem
{
    public class TurnHandler : ITurnHandler
    {
        public event Action OnSetNewCurrentTurn;
    
        private List<Turn> _turns;
        private int _currentTurnIndex;

        public Player CurrentPlayer => _turns[_currentTurnIndex].Player;

        public TurnHandler(List<Player> players)
        {
            _turns = new List<Turn>(players.Count);

            foreach (var player in players)
            {
                _turns.Add(new Turn(player));
            }
        }

        public void SetCurrentTurn(int turnIndex)
        {
            _currentTurnIndex = turnIndex;

            OnSetNewCurrentTurn?.Invoke();
        }

        public void MoveToNextTurn()
        {
            if (_currentTurnIndex >= _turns.Count - 1)
                _currentTurnIndex = 0;
            else
                _currentTurnIndex++;

            OnSetNewCurrentTurn?.Invoke();
        }
    }

    public interface ITurnHandler
    {
        public Player CurrentPlayer { get; }
    }
}

