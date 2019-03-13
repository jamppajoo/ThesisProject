using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSpeedBoostTileDestruction : MonoBehaviour {

    /// <summary>
    /// Adds speedboost amount and destructs the tile object 
    /// </summary>

    private LayerMask speedBoostLayer;
    private Tilemap tilemap;
    private BallSpeedBoost ballSpeedBoost;

    private void Awake()
    {
        ballSpeedBoost = GetComponent<BallSpeedBoost>();
        speedBoostLayer = LayerMask.NameToLayer("SpeedBoost");
        tilemap = GameObject.Find("Grid/SpeedBoost").GetComponent<Tilemap>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == speedBoostLayer)
        {
            Vector2 hitposition = Vector2.zero;
            hitposition = collision.transform.position;
            tilemap.SetTile(tilemap.WorldToCell(hitposition), null);
            Destroy(collision.gameObject);
            ballSpeedBoost.AddSpeedBoost();
        }
    }
}
