using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParallaxMovement : MonoBehaviour {

    public float multiplier = 1;
    private Vector3 cameraPosition;
    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = Camera.main.transform.position;
    }
    void Update()
    {
        cameraPosition = Camera.main.transform.position;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + (cameraPosition.x- lastCameraPosition.x) * multiplier, gameObject.transform.position.y, gameObject.transform.position.z);
        lastCameraPosition = cameraPosition;
    }

}
