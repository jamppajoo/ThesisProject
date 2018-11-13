using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomGridBrush(true, false, false, "Decoration Spawner Brush")]
    public class DecorationSpawnerBrush : GridBrush
    {
        [MenuItem("Assets/Create/Decoration Spawner Brush")]
        public static void CreateBrush()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Decoration Spawner Brush", "New Decoration Spawner Brush", "asset", "Save Decoration Spawner Brush", "Assets");

            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DecorationSpawnerBrush>(), path);
        }
        
    }

    [CustomEditor(typeof(DecorationSpawnerBrush))]
    [CanEditMultipleObjects]
    public class DecorationSpawnerBrushEditor : GridBrushEditor
    {
        private DecorationSpawnerBrush decorationSpawnerBrush { get { return target as DecorationSpawnerBrush; } }


        Tilemap decorationTilemap;
        Tilemap platformTilemap;
        TileBase decorationTile;
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Decoration tilemap layer");
            EditorGUILayout.LabelField("Platform tilemap layer");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            decorationTilemap = (Tilemap)EditorGUILayout.ObjectField(decorationTilemap, typeof(Tilemap), false);

            platformTilemap = (Tilemap)EditorGUILayout.ObjectField(platformTilemap, typeof(Tilemap), false);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Decoration tile");

            decorationTile = (TileBase)EditorGUILayout.ObjectField(decorationTile, typeof(TileBase), false);

            if (GUILayout.Button("Fill Decorations"))
            {
                
                Tilemap platformTilemapReference = GameObject.Find(platformTilemap.name).GetComponent<Tilemap>();
                Tilemap decorationTilemapReference = GameObject.Find(decorationTilemap.name).GetComponent<Tilemap>();

                for (int px = platformTilemapReference.cellBounds.xMin; px < platformTilemapReference.cellBounds.xMax; px++)
                {
                    for (int py = platformTilemapReference.cellBounds.yMin; py < platformTilemapReference.cellBounds.yMax; py++)
                    {
                        Vector3Int localPlace = (new Vector3Int(px, py, (int)platformTilemapReference.transform.position.y));
                        Vector3 place = platformTilemapReference.CellToWorld(localPlace);
                        if(platformTilemapReference.HasTile(localPlace))
                        {
                            decorationTilemapReference.SetTile(localPlace, null);
                        }
                    }

                }



            }
        }
    }
}
