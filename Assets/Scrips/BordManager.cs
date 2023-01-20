using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BordManager : MonoBehaviour
{
    [SerializeField] private List<TileUI> _tiles;

    private TileDataManager _tileDataManager;

    private void Awake()
    {
        _tileDataManager = new TileDataManager();
    }
    
    #region UnityEditorHelper
    
#if UNITY_EDITOR
    
    [Button("Get Tiles")]
    private void GetTiles()
    {
        foreach (var tileUI in transform.GetComponentsInChildren<TileUI>())
        {
            if (tileUI == null || _tiles.Contains(tileUI))
                continue;
            
            _tiles.Add(tileUI);
        }
    }


    
#endif

    #endregion    
    
}
