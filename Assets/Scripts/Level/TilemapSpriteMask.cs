using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapSpriteMask : MonoBehaviour
{
    Tilemap t;
    public Sprite tileMask;

    //void Start()
    //{
    //    t = GetComponent<Tilemap>();
    //    BoundsInt bounds = t.cellBounds;

    //    var s = t.layoutGrid.cellSize / 2;

    //    var availablePlaces = new List<Vector3>();

    //    for (int n = t.cellBounds.xMin; n < t.cellBounds.xMax; n++)
    //    {
    //        for (int p = t.cellBounds.yMin; p < t.cellBounds.yMax; p++)
    //        {
    //            Vector3Int localPlace = (new Vector3Int(n, p, (int)t.transform.position.y));
    //            Vector3 place = t.CellToWorld(localPlace);

    //            TileBase tile = t.GetTile(localPlace);
    //            CompositeCollider2D compositeCollider = t.GetComponent<CompositeCollider2D>();
    //            for (int i = 0; i < compositeCollider.pathCount; i++)
    //            {
    //                Vector2[] points = new Vector2[compositeCollider.GetPathPointCount(i)];
    //                Debug.Log("ASD: " + compositeCollider.pathCount);

    //            }

    //            if (tile)
    //            {
    //                availablePlaces.Add(place);

    //                var c = new GameObject().AddComponent<SpriteMask>();
    //                c.GetComponent<SpriteMask>().sprite = tileMask;
    //                c.transform.parent = t.transform;
    //                c.transform.localPosition = t.CellToLocal(localPlace) + s;

    //            }
    //        }
    //    }
    //}

    private void Start()
    {
        CompositeCollider2D compositeCollider = GetComponent<CompositeCollider2D>();
        for (int i = 0; i < compositeCollider.pathCount; i++)
        {
            Vector2[] points = new Vector2[compositeCollider.GetPathPointCount(i)];
            Debug.Log("ASD: " + compositeCollider.GetPath(i, points));
            Debug.Log("ASD: " + points[0] + points[1] + points[2] + points[3]);

            GameObject newMaskObject = new GameObject();
            SpriteMask newMask = newMaskObject.AddComponent<SpriteMask>();
            newMask.sprite = tileMask;
            
            Vector2 localScale = new Vector2(points[0].x - points[1].x, points[1].y - points[2].y);
            Vector2 localPosition = new Vector2(points[0].x - (points[0].x - points[1].x), 0);

            newMaskObject.transform.parent = gameObject.transform;
            newMaskObject.transform.localScale = localScale;
            newMaskObject.transform.localPosition = localPosition;


        }
    }

}
