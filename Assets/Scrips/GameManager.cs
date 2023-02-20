using System.Collections.Generic;
using DesignPatterns.Bord;
using DesignPatterns.Players;
using DesignPatterns.Tile.Data;
using DesignPatterns.TurnSystem;
using MoonActive.Algorithms;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<PlayerData> _playersDatas;
    [SerializeField] private InfoWindowHandler _infoWindow;
    [SerializeField] private BordManager _bordManager;

    private VictoryAlgorithm _victoryAlgorithm;
    
    private List<Player> _players;
    private TurnHandler _turnHandler;

    private void Awake()
    {
        _players = PlayerFactory.GetPlayers(_playersDatas.ToArray());
        
        _turnHandler = new TurnHandler(_players);
        
        _turnHandler.SetCurrentTurn(0);

        _victoryAlgorithm = new VictoryAlgorithm(3);
        
        _bordManager.Init(_turnHandler);

        _bordManager.OnTeySetTileData += TrySetTileData;
    }

    private void TrySetTileData(DropPoint dropPoint)
    {
        if (dropPoint.IsSucceeded)
        {
            var result = _victoryAlgorithm.CheckForVictory(TileDataManager.Instance.TileDatas,dropPoint,_turnHandler.CurrentPlayer.ID);

            if (result == _turnHandler.CurrentPlayer.ID)
            {
                Debug.Log("Win");    
                return;
            }
            
            NextTurn();
        }
    }

    [Button("NextTurn")]
    public void NextTurn()
    {
        _turnHandler.MoveToNextTurn();
        _infoWindow.SetTeamView(_turnHandler.CurrentPlayer);
    }

    private void OnDestroy()
    {
        _bordManager.OnTeySetTileData -= TrySetTileData;
    }
}
