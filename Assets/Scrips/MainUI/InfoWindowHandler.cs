using Scrips.TurnSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindowHandler : MonoBehaviour
{
   [SerializeField] private TMP_Text _teamName;
   [SerializeField] private TMP_Text _playerName;
   [SerializeField] private Image _image;

   public void SetTeamView(PlayerTurn playerTurn)
   {
      _playerName.text = playerTurn.PlayerName;
      _teamName.text = playerTurn.PlayerTeam.TeamName;
      _image.color = playerTurn.PlayerTeam.TeamColor;
   }
}
