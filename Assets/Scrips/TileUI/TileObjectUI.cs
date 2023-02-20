using DesignPatterns.Tile.Data;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Tile.UI
{
    public class TileObjectUI : MonoBehaviour
    {
        [SerializeField] private TileInput _input;

        [SerializeField] private Image _image;
        [SerializeField] private Vector2 _pos;
        
        public Vector2 Pos => _pos;

        public TileInput Input => _input;
        
        public void SetTile(TileData data)
        {
            if (data == null)
            {
                _image.sprite = null;
                return;
            }
            
            _image.sprite = data.Sprite;
        }
    }
}

