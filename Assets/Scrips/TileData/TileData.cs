using JonnixTools.Utilities.RectTransition;
using UnityEngine;


namespace DesignPatterns.Tile.Data
{
    [CreateAssetMenu(fileName = "New TileData",menuName = "ScriptableObjects/Tiles/New TileData")]
    public class TileData : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private TransitionPackSO _transition;

        public Sprite Sprite => _sprite;

        public TransitionPackSO Transition => _transition;
    }
}


