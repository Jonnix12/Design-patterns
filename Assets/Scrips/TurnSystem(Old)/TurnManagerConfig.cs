using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scrips.TurnSystem
{
    [CreateAssetMenu(fileName = "New Turn Manager Config",menuName = "ScriptableObjects/Turn System/New Turn Manager Config")]
    public class TurnManagerConfig : ScriptableObject
    {
        public static event Action OnTeamUpdate;

        [SerializeField] private List<Team> _teams;

        public  List<Team> Teams => _teams;
        private List<PlayerTurn> _allPlayers;
        public List<PlayerTurn> AllPlayers => _allPlayers;
        
        
        [Button("Add new Team")]
        public void AddTeam()
        {
            _teams.Add(new Team("New team"));
        }
        
        private void SetAllPlayerList()
        {
            _allPlayers = new List<PlayerTurn>();

            foreach (var player in _teams.SelectMany(team => team.TeamPlayers))
                _allPlayers.Add(player);
        }

        private void OnValidate()
        {
            SetAllPlayerList();
        }
    }
}