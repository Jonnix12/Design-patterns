using System;
using System.Collections.Generic;
using DesignPatterns.Bord.Command;
using DesignPatterns.Tile.Data;
using DesignPatterns.Tile.UI;
using DesignPatterns.TurnSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DesignPatterns.Bord
{
    [Serializable]
    public class BordManager : MonoBehaviour
    {
        public event Action<DropPoint> OnTeySetTileData;
        
        [SerializeField] private List<TileObjectUI> _tiles;
        
        private ITurnHandler _turnHandler;

        private CommandHandler _commandHandler;
        
        private void Awake()
        {
            _commandHandler = new CommandHandler();
            
            foreach (var objectUI in _tiles)
            {
                objectUI.Input.OnClickValue += ClickBorad;
            }
        }

        public void Init(ITurnHandler turnHandler)
        {
            _turnHandler = turnHandler;
        }

        private void ClickBorad(TileObjectUI objectUI)
        {
            _commandHandler.AddCommand(new AddObjectToBoradCommand(_turnHandler.CurrentPlayer,objectUI));
            
            _commandHandler.Execute(out bool isScceeded);
                                
            var dropPoint = new DropPoint((int)objectUI.Pos.y,(int)objectUI.Pos.x, isScceeded);
            
            OnTeySetTileData?.Invoke(dropPoint);
        }
        
        [ContextMenu("Undo")]
        public void Undo()
        {
            _commandHandler.Undo();
        }

        private void OnDestroy()
        {
            foreach (var objectUI in _tiles)
            {
                objectUI.Input.OnClickValue -= ClickBorad;
            }
        }

        #region UnityEditorHelper
#if UNITY_EDITOR
        [Button("Get Tiles")]
        private void GetTiles()
        {
            foreach (var tileUI in transform.GetComponentsInChildren<TileObjectUI>()) 
            {
                if (tileUI == null || _tiles.Contains(tileUI))
                    continue;
            
                _tiles.Add(tileUI);
            }
        }
#endif
        #endregion
    }

    public struct DropPoint
    {
        private int _raw;
        private int _colum;

        private bool _isSucceeded;

        public int Raw => _raw;

        public int Colum => _colum;

        public bool IsSucceeded => _isSucceeded;

        public DropPoint(int raw,int colum,bool isSucceeded)
        {
            _isSucceeded = isSucceeded;
            _raw = raw;
            _colum = colum;
        }
    }
}


