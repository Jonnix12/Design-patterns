using System;
using DesignPatterns.Players;

namespace DesignPatterns.TurnSystem
{
    public class Turn : IDisposable
    {
        private Player _player;

        private bool _isActiveTurn;

        public bool IsActiveTurn => _isActiveTurn;

        public Player Player => _player;
    
        public Turn(Player player)
        {
            _player = player;
        }

        public void Dispose()
        {
           
        }
    }

}
