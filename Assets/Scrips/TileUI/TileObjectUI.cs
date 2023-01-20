using System;
using UnityEngine;
using UnityEngine.UI;

public class TileObjectUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    public void SetObject(ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.X:
                //_image.sprite = _x;
                break;
            case ObjectType.O:
                //_image.sprite = _o;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(objectType), objectType, null);
        }
        Resources
    }
}

public enum ObjectType
{
    X,
    O
};
