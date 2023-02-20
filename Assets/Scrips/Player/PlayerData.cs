using System;
using DesignPatterns.Tile.Data;
using UnityEngine;

namespace DesignPatterns.Players
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private string _name;
        [SerializeField] private int _id;
        [SerializeField] private TileData _tileData;
       
        public string Name => _name;
        public TileData TileData => _tileData;
        public int ID => _id;
    }
}