﻿// ReSharper disable InconsistentNaming

using LDtkUnity.Providers;
using LDtkUnity.Tools;
using UnityEngine;

namespace LDtkUnity
{
    public partial class EntityInstanceTile
    {
        public TilesetDefinition Definition => LDtkProviderUid.GetUidData<TilesetDefinition>(TilesetUid);
        public Rect SourceRect => SrcRect.ToRect();
    }
}