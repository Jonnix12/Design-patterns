using JonnixTools.Utilities.RectTransition;
using UnityEngine;

[CreateAssetMenu(fileName = "New TileData",menuName = "ScriptableObjects/Tiles/New TileData")]
public class TileData : ScriptableObject
{
   [SerializeField] private Sprite _sprite;
   [SerializeField] private TransitionPackSO _transition;
}
