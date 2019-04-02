using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Tilemaps;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace MultiPlayer
{
    /// <summary>
    /// Adds speedboost amount and destructs the tile object 
    /// </summary>

    public class MP_PlayerSpeedBoostTileDestruction : MonoBehaviourPunCallbacks, IPunObservable
    {
        private LayerMask speedBoostLayer;
        private Tilemap tilemap;
        private MP_BallSpeedBoost ballSpeedBoost;

        [SerializeField]
        private List<NetWorkTileData> networkDestructTiles = new List<NetWorkTileData>();
        [SerializeField]
        private List<NetWorkTileData> networkDestructTilesReceived = new List<NetWorkTileData>();

        [System.Serializable]
        public class NetWorkTileData
        {
            public float hitPositionX;
            public float hitPositionY;
            public int instanceID;
        }

        private void Awake()
        {
            ballSpeedBoost = GetComponent<MP_BallSpeedBoost>();
            speedBoostLayer = LayerMask.NameToLayer("SpeedBoost");
            tilemap = GameObject.Find("Grid/SpeedBoost").GetComponent<Tilemap>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!photonView.IsMine)
                return;

            if (collision.gameObject.layer == speedBoostLayer)
            {
                Vector2 hitposition = Vector2.zero;
                hitposition = collision.transform.position;
                tilemap.SetTile(tilemap.WorldToCell(hitposition), null);

                NetWorkTileData tempdata = new NetWorkTileData();
                tempdata.hitPositionX = hitposition.x;
                tempdata.hitPositionY = hitposition.y;
                tempdata.instanceID = gameObject.GetInstanceID();

                networkDestructTiles.Add(tempdata);

                Destroy(collision.gameObject);
                ballSpeedBoost.AddSpeedBoost();
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            bool thereIsDataToSend;
            byte[] tileData;
            bool thereIsDataToSendNW;
            if (stream.IsWriting)
            {
                if (networkDestructTiles.Count > 0)
                {
                    thereIsDataToSend = true;
                    tileData = ConvertToBinary(networkDestructTiles);
                    networkDestructTiles.Clear();
                }
                else
                {
                    thereIsDataToSend = false;
                    tileData = null;
                }
                stream.SendNext(thereIsDataToSend);
                stream.SendNext(tileData);
            }
            else
            {
                thereIsDataToSendNW = (bool)stream.ReceiveNext();
                if (!thereIsDataToSendNW)
                    return;

                byte[] temp = (byte[])stream.ReceiveNext();
                networkDestructTilesReceived = ConvertFromBinary(temp);
                NetworkDestructTiles();
            }

        }
        private void NetworkDestructTiles()
        {
            if (networkDestructTilesReceived != null && networkDestructTilesReceived.Count > 0)
            {
                foreach (NetWorkTileData tileObject in networkDestructTilesReceived)
                {
                    tilemap.SetTile(tilemap.WorldToCell(new Vector3(tileObject.hitPositionX, tileObject.hitPositionY, 0)), null);
                    DestroyImmediate(HandyExtensions.FindObjectFromInstanceID(tileObject.instanceID), true);
                }
                networkDestructTilesReceived.Clear();
            }

        }

        private byte[] ConvertToBinary(List<NetWorkTileData> toConvert)
        {
            var mStream = new MemoryStream();

            var bf = new BinaryFormatter();

            bf.Serialize(mStream, toConvert);

            return mStream.ToArray();

        }
        private List<NetWorkTileData> ConvertFromBinary(byte[] binaryData)
        {
            if (binaryData != null && binaryData.Length > 0)
            {
                var mStream = new MemoryStream();
                var bf = new BinaryFormatter();

                mStream.Write(binaryData, 0, binaryData.Length);
                mStream.Position = 0;

                var returnList = bf.Deserialize(mStream) as List<NetWorkTileData>;

                return returnList;
            }
            else
                return null;
        }
    }
}
