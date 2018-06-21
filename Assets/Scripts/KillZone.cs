using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Camera.main.GetComponent<MultipleCameraTarget>().RemoveTarget(collision.transform);
    }


}
