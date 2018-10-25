﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class SpeedBoostCollider : MonoBehaviour
{
    Tilemap t;
    void Start()
    {
        t = GetComponent<Tilemap>();
        BoundsInt bounds = t.cellBounds;

        var s = t.layoutGrid.cellSize / 2;

        var availablePlaces = new List<Vector3>();

        for (int n = t.cellBounds.xMin; n < t.cellBounds.xMax; n++)
        {
            for (int p = t.cellBounds.yMin; p < t.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)t.transform.position.y));
                Vector3 place = t.CellToWorld(localPlace);

                var tile = t.GetTile(localPlace);

                if (tile)
                {
                    availablePlaces.Add(place);

                    var c = new GameObject().AddComponent<BoxCollider2D>();
                    c.isTrigger = true;
                    c.transform.parent = t.transform;
                    c.transform.localPosition = t.CellToLocal(localPlace) + s;
                    c.gameObject.layer = LayerMask.NameToLayer("SpeedBoost");
                    
                }
            }
        }
    }
}