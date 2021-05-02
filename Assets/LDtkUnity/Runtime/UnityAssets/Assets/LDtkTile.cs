﻿using UnityEngine;
using UnityEngine.Tilemaps;

namespace LDtkUnity
{
    /// <summary>
    /// The tile used for AutoLayers and Tile layers in LDtk. Not IntGridValues
    /// </summary>
    public class LDtkTile : TileBase
    {
        //public const string PROP_COLLIDER_TYPE = nameof(_colliderType);
        //public const string PROP_CUSTOM_PHYSICS_SPRITE = nameof(_customPhysicsSprite);
        
        public Sprite _sprite;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = _sprite;
            tileData.colliderType = Tile.ColliderType.None;
            
            //make color full, the tilemaps themselves have the correct opacity set.
            tileData.color = Color.white;
        }
    }
}