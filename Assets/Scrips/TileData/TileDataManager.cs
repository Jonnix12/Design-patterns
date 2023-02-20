using UnityEngine;

namespace DesignPatterns.Tile.Data
{
    public class TileDataManager
    {
        private static TileDataManager _instance;

        public static TileDataManager Instance
        {
            get { return _instance ??= new TileDataManager(); }
        }


        private int[,] _tileDatas;
        public int[,] TileDatas => _tileDatas;
   
        public TileDataManager()
        {
            _tileDatas = new int[3, 3];
        }


        public bool TrySetTile(Vector2 pos, int id)
        {
            if (_tileDatas[(int) pos.x, (int) pos.y] != 0)
                return false;
            
            _tileDatas[(int) pos.x, (int) pos.y] = id;
            return true;
        }

        public void RestTile(Vector2 pos)
        {
            _tileDatas[(int) pos.x, (int) pos.y] = 0;
        }
    }
}