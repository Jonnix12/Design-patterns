using Scrips.TurnSystem;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TurnManagerConfig _turnManagerConfig;
    [SerializeField] private InfoWindowHandler _infoWindow;
    
    private TurnManager _turnManager;

    private void Awake()
    {
        _turnManager = new TurnManager(_turnManagerConfig);
        
    }

    [Button("NextTurn")]
    public void NextTurn()
    {
        _turnManager.NextTurnTurns();
        _infoWindow.SetTeamView(_turnManager.CurrentPlayerTurn);
    }
}
