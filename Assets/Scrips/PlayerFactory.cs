using System.Collections.Generic;
using DesignPatterns.Players;

namespace DesignPatterns.Bord
{
    public class PlayerFactory
    {
        public static Player GetPlayer(PlayerData playerData)
        {
            return new Player(playerData);
        }
       
        public static List<Player> GetPlayers(params PlayerData[] playerDatas)
        {
            List<Player> output = new List<Player>(playerDatas.Length);
           
            foreach (var playerData in playerDatas)
            {
                output.Add(GetPlayer(playerData));
            }

            return output;
        }
    }
}