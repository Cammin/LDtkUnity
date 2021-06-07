﻿using UnityEditor;
using UnityEngine;

namespace LDtkUnity.Editor
{
    public class LDtkSectionMain : LDtkSectionDrawer
    {
        private LdtkJson _data;
        
        private static readonly GUIContent PixelsPerUnit = new GUIContent
        {
            text = "Main Pixels Per Unit",
            tooltip = "Dictates what all of the instantiated Tileset scales will adjust to, in case several LDtk layer's GridSize's are different."
        };
        private static readonly GUIContent Atlas = new GUIContent
        {
            text = "Sprite Atlas",
            tooltip = "Create your own Sprite Atlas and assign it here if desired.\n" +
                      "This solves the \"tearing\" in the sprites of the tilemaps.\n" +
                      "The sprite atlas is reserved for auto-generated sprites only. Any foreign sprites assigned to the atlas will be removed."
        };
        private static readonly GUIContent LevelFields = new GUIContent
        {
            text = "Custom Level Prefab",
            tooltip = "Optional.\n" +
                      "If assigned, will be in place of every GameObject for levels.\n" +
                      "Use for custom scripting via the interface events to store certain values, etc."
        };
        private static readonly GUIContent DeparentInRuntime = new GUIContent
        {
            text = "De-parent in Runtime",
            tooltip = "When on, adds components to the project, levels, and entity-layer GameObjects that act to de-parent all of their children in runtime.\n" +
                      "This results in increased runtime performance.\n" +
                      "Keep this on if the exact level/layer hierarchy structure is not a concern in runtime."
        };
        private static readonly GUIContent LogBuildTimes = new GUIContent
        {
            text = "Log Build Times",
            tooltip = "Use this to display the count of levels built, and how long it took to generate them."
        };
        private static readonly GUIContent IntGridVisible = new GUIContent()
        {
            text = "Render IntGrid Values",
            tooltip = "Use this if rendering the IntGrid value colors is preferred."
        };
        private static readonly GUIContent UseCompositeCollider = new GUIContent
        {
            text = "Use Composite Collider",
            tooltip = "Use this to add a CompositeCollider2D to all IntGrid tilemaps."
        };

        protected override string PropertyName => "";
        protected override string GuiText => "Main";
        protected override string GuiTooltip => "This is the importer menu.\n" +
                                                "Configure all of your custom settings here.";
        protected override Texture GuiImage => LDtkIconUtility.LoadFavIcon();
        protected override string ReferenceLink => "https://cammin.github.io/LDtkUnity/documentation/Importer/topic_Section_Main.html";

        protected override bool SupportsMultipleSelection => true;

        
        public LDtkSectionMain(SerializedObject serializedObject) : base(serializedObject)
        {
        }

        public void SetJson(LdtkJson data)
        {
            _data = data;
        }
        

        protected override void DrawDropdownContent()
        {
            if (_data == null)
            {
                return;
            }
            
            DrawField(PixelsPerUnit, LDtkProjectImporter.PIXELS_PER_UNIT);

            //draw the sprite atlas only if we have tiles to pack essentially
            if (!_data.Defs.Tilesets.IsNullOrEmpty())
            {
                DrawField(Atlas, LDtkProjectImporter.ATLAS);
            }

            SerializedProperty levelPrefabProp = DrawField(LevelFields, LDtkProjectImporter.CUSTOM_LEVEL_PREFAB);
            DenyPotentialResursiveGameObjects(levelPrefabProp);

            DrawField(DeparentInRuntime, LDtkProjectImporter.DEPARENT_IN_RUNTIME);
            DrawField(LogBuildTimes, LDtkProjectImporter.LOG_BUILD_TIMES);

            if (!_data.Defs.IntGridLayers.IsNullOrEmpty())
            {
                DrawField(IntGridVisible, LDtkProjectImporter.INTGRID_VISIBLE);
                DrawField(UseCompositeCollider, LDtkProjectImporter.USE_COMPOSITE_COLLIDER);
            }
        }

        private SerializedProperty DrawField(GUIContent content, string propName)
        {
            SerializedProperty prop = SerializedObject.FindProperty(propName);
            EditorGUILayout.PropertyField(prop, content);
            return prop;
        }
    }
}