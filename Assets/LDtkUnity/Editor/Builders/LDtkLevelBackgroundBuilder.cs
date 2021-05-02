﻿using System.Xml.Serialization;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace LDtkUnity.Editor.Builders
{
    public class LDtkLevelBackgroundBuilder
    {
        private readonly Transform _levelTransform;
        private readonly Level _level;
        private readonly Texture2D _texture;
        private readonly int _layerSortingOrder;
        private readonly int _pixelsPerUnit;
        


        public LDtkLevelBackgroundBuilder(Transform levelTransform, Level level, Texture2D imageSprite, int layerSortingOrder, int pixelsPerUnit)
        {
            _levelTransform = levelTransform;
            _level = level;
            _texture = imageSprite;
            _layerSortingOrder = layerSortingOrder;
            _pixelsPerUnit = pixelsPerUnit;

        }
        
        /// <returns>
        /// The sliced sprite result of the backdrop.
        /// </returns>
        public Sprite BuildBackground()
        {
            if (_texture == null)
            {
                Debug.LogError("null Sprite");
                return null;
            }

            Sprite sprite = GetSprite();
            if (sprite == null)
            {
                Debug.LogError("Sprite null");
                return null;
            }

            SpriteRenderer renderer = CreateGameObject();
            renderer.sprite = sprite;
            renderer.sortingOrder = _layerSortingOrder;
            
            ManipulateTransform(renderer.transform);
            
            //LDtkEditorUtil.Dirty(renderer);

            return sprite;
        }

        private void ManipulateTransform(Transform trans)
        {
            trans.parent = _levelTransform;

            Vector2 levelPosition = LDtkToolOriginCoordConverter.LevelBackgroundPosition(_level.BgPos.UnityTopLeftPx, _level.BgPos.UnityCropRect.height, _pixelsPerUnit, _level.BgPos.UnityScale.y);
            
            trans.localPosition = levelPosition;

            Vector2 scale = _level.BgPos.UnityScale;
            trans.localScale = new Vector3(scale.x, scale.y, 1);
            
            //LDtkEditorUtil.Dirty(trans);
        }

        private SpriteRenderer CreateGameObject()
        {
            GameObject go = new GameObject(_level.Identifier + "_Bg");
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            return renderer;
        }

        private Sprite GetSprite()
        {
            Rect rect = _level.BgPos.UnityCropRect;

            rect.position = LDtkToolOriginCoordConverter.LevelBackgroundImageSliceCoord(rect.position, _texture.height, rect.height);
            
            if (!LDtkTextureSpriteSlicer.IsLegalSpriteSlice(_texture, rect))
            {
                Debug.LogError($"Illegal Sprite slice {rect} from texture ({_texture.width}, {_texture.height})");
                return null;
            }

            Sprite sprite = Sprite.Create(_texture, rect, Vector2.up, _pixelsPerUnit);
            
            sprite.name = _texture.name;
            //Debug.Log($"Sprite {sprite}");
            return sprite;
        }

        
    }
}