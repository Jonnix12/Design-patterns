
namespace DesignPatterns.Tile.Data
{
    public class Tile
    {
        private int _objectId;

        public bool IsHaveValue => _objectId != 0;
        public int ObjectId => _objectId;
        
        public Tile()
        {
            _objectId = 0;
        }

        public void UpdateTile(int id)
        {
            _objectId = id;
        }
    }
}