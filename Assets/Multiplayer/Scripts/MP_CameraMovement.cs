using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MultiPlayer
{
    [RequireComponent(typeof(Camera))]
    public class MP_CameraMovement : MonoBehaviourPunCallbacks, IPunObservable
    {
        public List<Transform> targets;

        public Vector3 offset;

        public float smoothTime = 0.5f;
        private Vector3 velocity;

        public float maxZoom = 40f;

        public float minZoom = 10f;

        public float zoomLimiter = 50f;

        public float distanceToDropLastPlayer = 45f;
        private Camera mainCam;
        private LevelManager levelManager;

        private void Awake()
        {
            mainCam = GetComponent<Camera>();
            levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }

        private void LateUpdate()
        {
            if (!photonView.IsMine)
                return;

            if (targets.Count == 0)
                return;
            Move();
            Zoom();
            if (GetGreatestDistance() > distanceToDropLastPlayer)
            {
                DropLastPlayer();
            }
        }
        private void Move()
        {

            Vector3 centerPoint = GetCenterPoint();

            Vector3 newPosition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        }
        private void Zoom()
        {
            float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, newZoom, Time.deltaTime);
        }
        private void DropLastPlayer()
        {
            RemoveTarget(GetLastPlayer());
            if (targets.Count == 1)
                levelManager.Winner(targets[0].gameObject);
        }
        private float GetGreatestDistance()
        {
            var bounds = new Bounds(targets[0].position, Vector3.zero);
            for (int i = 0; i < targets.Count; i++)
            {
                bounds.Encapsulate(targets[i].position);
            }
            return bounds.size.x;
        }
        private Vector3 GetCenterPoint()
        {
            if (targets.Count == 1)
            {
                return targets[0].position;
            }

            var bounds = new Bounds(targets[0].position, Vector3.zero);
            for (int i = 0; i < targets.Count; i++)
            {
                bounds.Encapsulate(targets[i].position);

            }
            return bounds.center;
        }
        private Vector3 GetLeadingPlayer()
        {
            Vector3 leadingPosition = Vector3.zero;
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].position.x > leadingPosition.x)
                {
                    leadingPosition = targets[i].position;
                }
            }
            return leadingPosition;
        }

        private Transform GetLastPlayer()
        {
            Vector3 lastPosition = GetLeadingPlayer();
            Transform lastPlayer = null;
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].position.x < lastPosition.x)
                {
                    lastPosition = targets[i].position;
                    lastPlayer = targets[i];
                }
            }
            return lastPlayer;
        }
        public void RemoveTarget(Transform target)
        {
            targets.Remove(target);
            Destroy(target.gameObject);
        }
        public void AddTarget(Transform target)
        {
            targets.Add(target);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.IsWriting)
            {
                stream.SendNext(mainCam.orthographicSize);
            }
            else
            {
                mainCam.orthographicSize = (float) stream.ReceiveNext();
            }
        }
    }
}
