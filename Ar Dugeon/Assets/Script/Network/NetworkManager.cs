using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class NetworkManager : Photon.PunBehaviour
{

    public Text connectionParameters;
    public Text roomName;
    public bool inRoom = false;
    public Canvas canvas;
    private ListRooms _listRooms;

    void Start ()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        _listRooms = canvas.GetComponent<ListRooms>();
    }

	void Update ()
    {
        connectionParameters.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public override void OnJoinedLobby()
    {
        //PhotonNetwork.JoinRandomRoom();
    }

    public void CreateRoom()
    {
        if(!inRoom)
            PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { maxPlayers = 4 }, null);
        inRoom = true;
    }

    public override void OnReceivedRoomListUpdate()
    {
        Debug.Log("OnreceiveRoomList");
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
    }

    public void JoinRoom()
    {
        if(!inRoom)
            PhotonNetwork.JoinRoom(_listRooms.selectedRoomName);
        inRoom = true;
    }
    
    public override void OnJoinedRoom()
    {

    }

    public void LoadScene()
    {
        this.photonView.RPC("LoadSceneForEach", PhotonTargets.All);
    }

    [PunRPC]
    void LoadSceneForEach(PhotonMessageInfo info)
    {
        Application.LoadLevel("UI_Scene");
        Debug.Log(string.Format("Info: {0} {1} {2}", info.sender, info.photonView, info.timestamp));
    }
}
