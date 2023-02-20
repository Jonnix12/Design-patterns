using DesignPatterns.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindowHandler : MonoBehaviour
{
   [SerializeField] private TMP_Text _teamName;
   [SerializeField] private TMP_Text _playerName;
   [SerializeField] private Image _image;

   public void SetTeamView(Player player)
   {
      _playerName.text = player.Name;
   }
}
