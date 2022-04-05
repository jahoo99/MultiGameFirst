using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
namespace Com.MyCompany.MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
        [SerializeField] private byte _maxPlayersPerRoom = 4;
        #endregion

        #region Private Fields

        string gameVersion = "1";

        #endregion
        #region MonoBehaviour Callbacks
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        private void Start()
        {
            Connect();
        }
        #endregion

        #region Public Methods

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinRandomRoom();
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarning("OnDisconnected");
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed");
            PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = _maxPlayersPerRoom });
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
        }

        #endregion
    }
}

