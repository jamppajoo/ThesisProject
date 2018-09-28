using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor
{
    [CustomGridBrush(true, false, false, "Custom Brush")]
    public class CustomBrush : GridBrush
    {
        public Vector3 startPosition;

        [MenuItem("Assets/Create/Custom Brush")]
        public static void CreateBrush()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Custom Brush", "New Custom Brush", "asset", "Save Custom Brush", "Assets");

            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<CustomBrush>(), path);
        }
    }

    [CustomEditor(typeof(CustomBrush))]
    public class CustomBrushEditor : GridBrushEditor
    {
        private CustomBrush customBrush { get { return target as CustomBrush; } }

        public override void OnPaintSceneGUI(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, GridBrushBase.Tool tool, bool executing)
        {
            base.OnPaintSceneGUI(gridLayout, brushTarget, position, tool, executing);
        }


    }

}
