using System;
using DesignPatterns.Tile.Data;

namespace DesignPatterns.Players
{
    public class Player : IPlayer
    {
        private readonly PlayerData _data;
        
        public string Name => _data.Name;

        public int ID => _data.ID;

        public TileData TileData => _data.TileData;           
        
        public Player(PlayerData playerData)
        {
            _data = playerData;
        }
    }
}



