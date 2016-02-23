using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;
using System;
using System.Collections.Generic;


public class NetworkManager : Photon.PunBehaviour
{

    public Text connectionParameters;
    public Text playerName;
    public Text roomName;
    public Text nbPlayers;
    public Text feedback;
    public bool inRoom = false;
    public Canvas canvas;
    private ListRooms _listRooms;
    private ListModels _listModels;

    public List<string[]> playersData;
    public string[] data;
    public Transform panel;
    private List<GameObject> playerList;

    void Start ()
    {
        DontDestroyOnLoad(this);
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        _listRooms = canvas.GetComponent<ListRooms>();
        _listModels = canvas.GetComponent<ListModels>();
        playersData = new List<string[]>();
        data = new string[3];
        feedback.text = "";
        feedback.GetComponent<Text>().color = Color.red;
    }

	void Update ()
    {
        connectionParameters.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public void CreateRoom()
    {
        if(roomName.text != "" && playerName.text != "" && nbPlayers.text != "" && !inRoom)
        {
            int nb = int.Parse(nbPlayers.text);
            PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { maxPlayers = 0 }, null);
           
            PhotonNetwork.player.name = playerName.text;
            inRoom = true;
            feedback.text = ""; 
        }
        if(roomName.text == "" && playerName.text == "" && nbPlayers.text == "")
            feedback.text = "Enter a player name, a room name and the number of players";
        else if (playerName.text == "" )
            feedback.text = "Enter a player name";
        else if (roomName.text == "")
            feedback.text = "Enter a room name";
        else if (nbPlayers.text == "")
            feedback.text = "Enter number of players";
    }
    
    public void JoinRoom()
    {
        if(playerName.text != "" && _listRooms.selectedRoomName != "" && !inRoom)
        {
            PhotonNetwork.JoinRoom(_listRooms.selectedRoomName);
            PhotonNetwork.player.name = playerName.text;
            inRoom = true;
            feedback.text = "";
        }
        else if (playerName.text == "" && _listRooms.selectedRoomName == "")
            feedback.text = "Enter a player name and select a room";
        else if(playerName.text == "")
            feedback.text = "Enter a player name";
        else if (_listRooms.selectedRoomName == "")
            feedback.text = "Select a room";
    }
    public void LoadScene()
    {
        this.photonView.RPC("LoadSceneForEach", PhotonTargets.All);
    }

    [PunRPC]
    void LoadSceneForEach(PhotonMessageInfo info)
    {
        Application.LoadLevel("Main_Scene");
        data[0] = PhotonNetwork.playerName;
        data[1] = _listModels.selectedModelName;
        playersData.Add(data);
        Debug.Log(string.Format("Info: {0} {1} {2}", info.sender, info.photonView, info.timestamp));
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.room.maxPlayers = int.Parse(nbPlayers.text);
    }
    public override void OnJoinedLobby()
    {
        //PhotonNetwork.JoinRandomRoom();
    }

    public override void OnReceivedRoomListUpdate()
    {
        Debug.Log("OnreceiveRoomList");
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        GameObject gameobject = (GameObject)Instantiate(Resources.Load("PlayerObject"));
        gameobject.transform.SetParent(panel, false);
        gameobject.GetComponentInChildren<Text>().text = newPlayer.name;
    }
}
