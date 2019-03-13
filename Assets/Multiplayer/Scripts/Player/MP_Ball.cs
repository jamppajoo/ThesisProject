using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiPlayer
{
    public class MP_Ball : MonoBehaviour
    {
        public string playerID;

        private void Start()
        {
            Camera.main.GetComponent<MP_CameraMovement>().AddTarget(gameObject.transform);
        }
    }
}
