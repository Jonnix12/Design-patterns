using System;
using UnityEngine;

public class TileDataManager
{
   private TileData[] _tileDatas;
   
   public TileDataManager()
   {
      _tileDatas = Resources.LoadAll<TileData>("/TileData");
   }

   public TileData GetTileData(ObjectType objectType)
   {
      switch (objectType)
      {
         case ObjectType.X:
            return _tileDatas[0];
         case ObjectType.O:
            return _tileDatas[1];
         default:
            throw new ArgumentOutOfRangeException(nameof(objectType), objectType, null);
      }
   }
}
