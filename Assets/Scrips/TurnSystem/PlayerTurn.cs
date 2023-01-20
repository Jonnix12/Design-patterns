using System;
using System.IO;
using UnityEngine;

namespace Scrips.TurnSystem
{
    [Serializable]
    public class PlayerTurn
    {
        public event Action<PlayerTurn> OnEnterTurn;
        public event Action<PlayerTurn> OnExitTurn;
        
        [SerializeField] private string _playerName;

        private TurnLogic _logic;

        private TurnState _turnState;
        
        public string PlayerName => _playerName;
        public Team PlayerTeam { get; private set; }

        public PlayerTurn(string playerName,Team team)
        {
            _playerName = playerName;
            PlayerTeam = team;
            FileStream fileStream = new FileStream(Application.streamingAssetsPath + "/TurnLogicImplamtesan", FileMode.CreateNew);
            
        }

        public void EnterTurn()
        {
            if (_turnState == TurnState.Active)
                return;

            _turnState = TurnState.Active;
            _logic.EnterTurnLogic();
            OnEnterTurn?.Invoke(this);
        }

        public void ExitTurn()
        {
            if (_turnState == TurnState.Deactive)
                return;

            _turnState = TurnState.Deactive;
            _logic.ExitTurnLogic();
            OnExitTurn?.Invoke(this);
        }
    }

    public enum TurnState
    {
        Active,
        Deactive
    }
}