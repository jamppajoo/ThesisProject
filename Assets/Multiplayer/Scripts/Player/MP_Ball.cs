using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiPlayer
{
    public class MP_Ball : MonoBehaviour
    {
        public string playerID = "P1";

        [HideInInspector]
        public LayerMask myLayerMask;

        private void Awake()
        {
            myLayerMask = gameObject.layer;
        }
        private void Start()
        {
            Camera.main.GetComponent<MP_CameraMovement>().AddTarget(gameObject.transform);
        }
    }
}
