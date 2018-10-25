using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
#if UNITY_EDITOR

    [CustomGridBrush(false, false, false, "CustomBrush")]
    public class CustomBrush : GridBrush
    {

        [MenuItem("Assets/Create/Tiles/CustomBrush", false, 0)]
        //This Function is called when you click the menu entry
        private static void CreateAcidBrush()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save CustomBrush", "New CustomBrush", "asset", "Save CustomBrush", "Assets");
            if (path == "")
                return;
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<CustomBrush>(), path);

        }


        public const string k_PlatformLayerName = "Platform";
        public const string k_IceLayerName = "Ice";
        public const string k_EarthLayerName = "Earth";
        public const string k_WallLayerName = "Walls";
        public const string k_LavaLayerName = "Lava";
        public TileBase[] tileBases;

        public GameObject iceParticleEffect;

        public GameObject lavaParticleEffect;

        public static string k_currentLayer;

        public static TileBase m_currentTileBase;

        public static Tilemap platform;

        private List<GameObject> iceParticeleEffects;
        private List<GameObject> lavaParticeleEffects;


        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            switchTileBase();
            PaintInternal(position, m_currentTileBase);
            if(m_currentTileBase == tileBases[1])
            {
                iceParticeleEffects.Add(Instantiate(iceParticleEffect, new Vector3(position.x + 0.5f, position.y + 1, position.z), Quaternion.identity, platform.transform));
            }
            else if (m_currentTileBase == tileBases[4])
            {
                lavaParticeleEffects.Add(Instantiate(lavaParticleEffect, new Vector3(position.x + 0.5f, position.y + 1, position.z), Quaternion.identity, platform.transform));
            }
        }
        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            switchTileBase();
            PaintInternalBox(position, m_currentTileBase);
        }

        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            platform = GetPlatform(k_currentLayer);
            platform.SetTile(position, null);
            base.Erase(gridLayout, brushTarget, position);
        }
        public override void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            platform = GetPlatform(k_currentLayer);
            TileBase[] tileArray = new TileBase[position.size.x * position.size.y * position.size.z];
            for (int index = 0; index < tileArray.Length; index++)
            {
                tileArray[index] = null;
            }
            platform.SetTilesBlock(position, tileArray);
            base.BoxErase(gridLayout, brushTarget, position);
        }
        private void switchTileBase()
        {
            platform = GetPlatform(k_currentLayer);
            if (platform != null)
            {

                switch (k_currentLayer)
                {
                    case k_PlatformLayerName:
                        m_currentTileBase = tileBases[0];
                        break;
                    case k_IceLayerName:
                        m_currentTileBase = tileBases[1];
                        break;
                    case k_WallLayerName:
                        m_currentTileBase = tileBases[2];
                        break;
                    case k_EarthLayerName:
                        m_currentTileBase = tileBases[3];
                        break;
                    case k_LavaLayerName:
                        m_currentTileBase = tileBases[4];
                        break;
                    default:
                        Debug.Log("Error changing the tilemap");
                        break;
                }
            }
        }
        private void PaintInternal(Vector3Int position, TileBase currentTileBase)
        {
            platform.SetTile(position, currentTileBase);
        }
        private void PaintInternalBox(BoundsInt position, TileBase currentTileBase)
        {
            TileBase[] tileArray = new TileBase[position.size.x * position.size.y * position.size.z];
            for (int index = 0; index < tileArray.Length; index++)
            {
                tileArray[index] = currentTileBase;
            }
            platform.SetTilesBlock(position, tileArray);
        }

        public static Tilemap GetPlatform(string layer)
        {
            GameObject go = GameObject.Find(layer);
            if (go != null)
                return go.GetComponent<Tilemap>();
            else
                return null;
        }

        public static void setTilemap(string layerName)
        {
            k_currentLayer = layerName;
        }
    }

    [CustomEditor(typeof(CustomBrush))]
    public class CustomBrushEditor : GridBrushEditor
    {
        //TODO koita vaihtaa tässä k_currentlayer oikeaksi
        private CustomBrush customBrush { get { return target as CustomBrush; } }
        public override void OnPaintSceneGUI(GridLayout grid, GameObject brushTarget, BoundsInt position, GridBrushBase.Tool tool, bool executing)
        {
            base.OnPaintSceneGUI(grid, brushTarget, position, tool, executing);
        }
    }

#endif
}
