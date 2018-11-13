using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomGridBrush(false, false, true, "Tilemap Changer Brush")]
    public class TilemapChangerBrush : GridBrush
    {
        [Serializable]
        public class TilemapConnection
        {
            public Tilemap connectionTilemap;
            public TileBase connectionTileBase;
        }

        [MenuItem("Assets/Create/Tilemap Changer Brush")]
        public static void CreateBrush()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Tilemap Changer Brush", "New Tilemap Changer Brush", "asset", "Save Tilemap Changer Brush", "Assets");

            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TilemapChangerBrush>(), path);
        }

        [SerializeField]
        public List<TilemapConnection> tilemapConnections = new List<TilemapConnection>();
        private TileBase tempTileBase;


        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            brushTarget = ReturnTilemapLayer(brushTarget);
            base.Paint(gridLayout, brushTarget, position);
        }
        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            brushTarget = ReturnTilemapLayer(brushTarget);
            base.Erase(gridLayout, brushTarget, position);
        }
        public override void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            brushTarget = ReturnTilemapLayer(brushTarget);
            base.BoxErase(gridLayout, brushTarget, position);
        }
        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            brushTarget = ReturnTilemapLayer(brushTarget);
            base.BoxFill(gridLayout, brushTarget, position);
        }
        public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            brushTarget = ReturnTilemapLayer(brushTarget);
            base.FloodFill(gridLayout, brushTarget, position);
        }

        private GameObject ReturnTilemapLayer(GameObject brushTarget)
        {
            tempTileBase = base.cells[base.cellCount - 1].tile;

            foreach (var item in tilemapConnections)
            {
                if (tempTileBase == item.connectionTileBase)
                {
                    brushTarget = GameObject.Find(item.connectionTilemap.gameObject.name).gameObject;
                }
            }
            return brushTarget;
        }
    }

    [CustomEditor(typeof(TilemapChangerBrush))]
    [CanEditMultipleObjects]
    public class TilemapChangerBrushEditor : GridBrushEditor
    {
        private TilemapChangerBrush tilemapChangerBrush { get { return target as TilemapChangerBrush; } }
        
        SerializedObject GetTarget;
        SerializedProperty ThisList;
        public override void OnInspectorGUI()
        {
            List<GameObject> prefabGameObjects = new List<GameObject>();
            GameObject[] sceneGameObjects = new GameObject[0];
            

            if (GUILayout.Button("Check Prefabs"))
            {
                prefabGameObjects = LoadAllPrefabsOfType(@"Assets\Prefabs\Essential");
                sceneGameObjects = FindObjectsOfType<GameObject>();
                foreach (GameObject prefab in prefabGameObjects)
                {

                    if (!GameObject.Find(prefab.name))
                    {
                        GameObject instatedObject = Instantiate(prefab);
                        instatedObject.name = prefab.name;
                    }
                        
                }
            }
            GetTarget = new SerializedObject(tilemapChangerBrush);
            ThisList = GetTarget.FindProperty("tilemapConnections");
            GetTarget.Update();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            for (int i = 0; i < ThisList.arraySize; i++)
            {

                SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex(i);

                SerializedProperty myConnectionTilemap = MyListRef.FindPropertyRelative("connectionTilemap");
                SerializedProperty myConnectionTileBase = MyListRef.FindPropertyRelative("connectionTileBase");

                // Choose to display automatic or custom field types. This is only for example to help display automatic and custom fields.
                EditorGUILayout.LabelField("Tilemap that you want to place the tile, tile that you want to place");
                EditorGUILayout.BeginHorizontal();
                myConnectionTilemap.objectReferenceValue = (Tilemap)EditorGUILayout.ObjectField(myConnectionTilemap.objectReferenceValue, typeof(Tilemap), false);
                myConnectionTileBase.objectReferenceValue = (TileBase)EditorGUILayout.ObjectField(myConnectionTileBase.objectReferenceValue, typeof(TileBase), false);
                if (GUILayout.Button("Remove This Index (" + i.ToString() + ")", GUILayout.Width(200)))
                {
                    ThisList.DeleteArrayElementAtIndex(i);
                }
                EditorGUILayout.EndHorizontal();
                //EditorGUILayout.BeginHorizontal();
                //EditorGUIUtility.labelWidth = 50;

                //EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            }

            EditorGUILayout.Space();
            //Or add a new item to the List<> with a button
            EditorGUILayout.LabelField("Add a new item with a button");

            if (GUILayout.Button("Add New"))
            {
                tilemapChangerBrush.tilemapConnections.Add(new TilemapChangerBrush.TilemapConnection());
            }
            //Apply the changes to our list
            GetTarget.ApplyModifiedProperties();
        }
        
        private List<GameObject> LoadAllPrefabsOfType(string path)         {
            if (path != "")
            {
                if (path.EndsWith("/"))
                {
                    path = path.TrimEnd('/');
                }
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");

            //loop through directory loading the game object and checking if it has the component you want
            List<GameObject> prefabComponents = new List<GameObject>();
            foreach (FileInfo fileInfo in fileInf)
            {
                string fullPath = fileInfo.FullName.Replace(@"\", "/");
                string assetPath = "Assets" + fullPath.Replace(Application.dataPath, "");
                GameObject prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;

                if (prefab != null)
                {
                    prefabComponents.Add(prefab);
                }
            }
            return prefabComponents;
        }
    }
}
