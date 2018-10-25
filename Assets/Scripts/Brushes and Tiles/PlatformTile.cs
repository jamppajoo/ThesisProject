using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformTile : Tile {

    [SerializeField]
    private Sprite[] surroundSprites;

    [SerializeField]
    private Sprite preview;

#if UNITY_EDITOR

    [MenuItem("Assets/Create/Tiles/PlatformTile")]
    public static void createTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save PlatformTile", "New PlatformTile", "asset", "Save PlatformTile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<PlatformTile>(), path);
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);
                // Debug.Log("x: " + x+ " y: " + y +  "Tile: " + tilemap.GetTile(nPos));
                tilemap.RefreshTile(nPos);
            }
        }
         
    }
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        Sprite choosedSprite = preview;
        string composition = string.Empty;
         for (int x = -1; x <= 1; x++)
         {
             for (int y = -1; y <= 1; y++)
             {
                 if( x != 0 || y != 0)
                 {
                     if (isEndBlock(tilemap, new Vector3Int(position.x + x, position.y + y, position.z), ref tileData))
                     {
                         composition += "P";
                     }
                     else
                         composition += "E";
                 }

             }
        }
        if ((composition == "EPEEEEEE" && !isThereABlock(tilemap, new Vector3Int(position.x + 1, position.y, position.z), CustomBrush.k_EarthLayerName))
            || composition == "EPEEEPEE" || composition == "EEPEEPEE")
        {
            //platform ending right
            if (!isThereABlock(tilemap, new Vector3Int(position.x, position.y - 1, position.z), CustomBrush.k_WallLayerName) && (composition != "EPEEEPEE" ) && composition != "EEPEEPEE"|| isThereABlock(tilemap, new Vector3Int(position.x + 1, position.y, position.z), CustomBrush.k_LavaLayerName))
                choosedSprite = preview;
            else
                choosedSprite = surroundSprites[1];
        }
        else if (composition == "EEEEEEPE" && !isThereABlock(tilemap, new Vector3Int(position.x - 1, position.y, position.z), CustomBrush.k_EarthLayerName)
            || composition == "PEEEEEPE" || composition == "PEEEEEEP")
        {
            //platform ending left
            if (!isThereABlock(tilemap, new Vector3Int(position.x, position.y - 1, position.z), CustomBrush.k_WallLayerName) && composition != "PEEEEEPE" && composition != "PEEEEEEP")
                choosedSprite = preview;
            else
                choosedSprite = surroundSprites[0];
        }
        else
            choosedSprite = preview;

        Transform root = tilemap.GetComponent<Tilemap>().transform.parent;
        
        Tilemap platform = null;

        if (root != null)
        {
            Transform platformGo = root.Find(CustomBrush.k_PlatformLayerName);
            if (platformGo != null)
            {
                CustomBrush.setTilemap(CustomBrush.k_PlatformLayerName);
                platform = platformGo.GetComponent<Tilemap>();

                tileData.sprite = choosedSprite;
                tileData.flags = TileFlags.LockAll;
                tileData.colliderType = ColliderType.Sprite;
            }
        }
        

    }


    private bool isThereABlock(ITilemap tilemap, Vector3Int position, string layer)
    {
        Transform root = tilemap.GetComponent<Tilemap>().transform.parent;

        Tilemap platform = null;

        if (root != null)
        {
            Transform platformGo = root.Find(layer);
            if (platformGo != null)
            {
                platform = platformGo.GetComponent<Tilemap>();
                if (platform.GetTile(new Vector3Int(position.x  , position.y, position.z)))
                {
                    return true;
                }
                else
                    return false;

            }
        }
        return false;
    }
    

    private bool isEndBlock(ITilemap tilemap, Vector3Int position, ref TileData tileData)
    {
        
        TileBase tile = tilemap.GetTile(position);
        // Debug.Log("x: " + position.x + " y: " + position.y + " tile: " + CustomBrush.getBlockLayer(CustomBrush.k_PlatformLayerName, position));
        return (tile != null && tile == this);
       
    }
#endif
}
