using DesignPatterns.Players;
using DesignPatterns.Tile.Data;
using DesignPatterns.Tile.UI;

namespace DesignPatterns.Bord.Command
{
    public class AddObjectToBoradCommand : ICommand
    {
        private Player _player;
        private TileObjectUI _tileObjectUI;
        
        public AddObjectToBoradCommand(Player player,TileObjectUI objectUI)
        {
            _player = player;
            _tileObjectUI = objectUI;
        }
        
        public void Execute(out bool isScceeded)
        {
            if (TileDataManager.Instance.TrySetTile(_tileObjectUI.Pos, _player.ID))
            {
                isScceeded = true;
                _tileObjectUI.SetTile(_player.TileData);
                return;
            }

            isScceeded = false;
        }

        public void Undo()
        {
            TileDataManager.Instance.RestTile(_tileObjectUI.Pos);
            _tileObjectUI.SetTile(null);
        }
    }
}