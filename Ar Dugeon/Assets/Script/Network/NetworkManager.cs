﻿using UnityEngine;
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

    private UIManagerLobby uiManager;
    public string[] playerData;

    public List<string[]> ObjectMasterData;
    public List<string[]> MarkersData;
    public string[] markerData;
    public Transform panel;
    public Transform objectsMasterPanel;
    private List<GameObject> playerList;

    void Start ()
    {
        DontDestroyOnLoad(this);
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        _listRooms = canvas.GetComponent<ListRooms>();
        _listModels = canvas.GetComponent<ListModels>();
        uiManager = GetComponent<UIManagerLobby>();
        playerData = new string[2];

        MarkersData = new List<string[]>();
        ObjectMasterData = new List<string[]>();

        markerData = new string[2];
        feedback.text = "";
        feedback.GetComponent<Text>().color = Color.red;
    }

	void Update ()
    {
        //connectionParameters.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public void CreateRoom()
    {
        if(roomName.text != "" && playerName.text != "" && nbPlayers.text != "" && !inRoom)
        {
            int nb = int.Parse(nbPlayers.text);
            byte value = (byte)nb;
            PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { maxPlayers = value }, null);
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
        int nbChilds = panel.childCount;
        for (int i = 0; i < nbChilds; i++)
        {
            Transform playerObject = panel.GetChild(i);
            string name = playerObject.GetComponentInChildren<Text>().text;
            string nameMarker = playerObject.GetComponentInChildren<Dropdown>().captionText.text;
            string[] modelData = new string[] { name, nameMarker };
            MarkersData.Add(modelData);
        }
        LoadObjectsMaster();
        this.photonView.RPC("LoadSceneForEach", PhotonTargets.All);
    }


    public void LoadObjectsMaster()
    {
        int nbChilds = objectsMasterPanel.childCount;
        for (int i = 0; i < nbChilds; i++)
        {
            Transform masterObject = objectsMasterPanel.GetChild(i);
            Transform child = masterObject.GetChild(0);
            string name = masterObject.GetChild(0).GetChild(0).gameObject.GetComponent<Dropdown>().captionText.text;
            string nameMarker = masterObject.GetChild(1).GetChild(0).gameObject.GetComponent<Dropdown>().captionText.text;
            string[] objectData = new string[] {name, nameMarker};
            ObjectMasterData.Add(objectData);
        }
    }

    [PunRPC]
    void LoadSceneForEach(PhotonMessageInfo info)
    {
        uiManager.enabled = false;
        Application.LoadLevel("MainScene");
        playerData[0] = PhotonNetwork.playerName;
        if(PhotonNetwork.isMasterClient)
            playerData[1] = uiManager.FieldName;
        else
            playerData[1] = uiManager.ModelName;
        Debug.Log(string.Format("Info: {0} {1} {2}", info.sender, info.photonView, info.timestamp));
    }

    [PunRPC]
    void SetParenting(string nameMarker, string modelName)
    {
        GameObject sceneRoot = GameObject.Find("Scene root");
        Transform marker = sceneRoot.transform.Find(nameMarker);
        if (marker != null)
        {
            GameObject gameobject = GameObject.Instantiate(Resources.Load(modelName)) as GameObject;
            gameobject.transform.SetParent(marker, false);
            gameObject.SetActive(false);
        }
    }

    [PunRPC]
    void LoadModelMarker(string playerName, string modelName)
    {
        Transform marker = null;
        string nameMarker = "";

        GameObject sceneRoot = GameObject.Find("Scene root");
        nameMarker = FindMarker(playerName);
        marker = sceneRoot.transform.Find(nameMarker);

        if(marker != null)
        {
            GameObject gameobject = GameObject.Instantiate(Resources.Load(modelName)) as GameObject;
            gameobject.transform.SetParent(marker, false);
            gameObject.SetActive(false);
            this.photonView.RPC("SetParenting", PhotonTargets.Others, nameMarker, modelName);
        }
    }

    private string FindMarker(string name)
    {
        for(int i = 0; i < MarkersData.Count; i++)
        {
            if (MarkersData[i][0] == name)
                return MarkersData[i][1];
        }

        return null;
    }

    [PunRPC]
    void LoadField(string fieldName)
    {
        Transform marker = null;
        GameObject sceneRoot = GameObject.Find("Scene root");
        marker = sceneRoot.transform.Find("Field");

        if (marker != null)
        {
            GameObject gameobject = GameObject.Instantiate(Resources.Load(fieldName)) as GameObject;
            gameobject.transform.SetParent(marker, false);
        }
    }

    [PunRPC]
    void LoadObjectServer(string objectName, string markerName)
    {
        Transform marker = null;
        GameObject sceneRoot = GameObject.Find("Scene root");
        marker = sceneRoot.transform.Find(markerName);
        if (marker != null)
        {
            GameObject gameobject = GameObject.Instantiate(Resources.Load(objectName)) as GameObject;
            gameobject.transform.SetParent(marker, false);
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        GameObject gameobject = (GameObject)Instantiate(Resources.Load("PlayerObject"));
        gameobject.transform.SetParent(panel, false);
        gameobject.GetComponentInChildren<Text>().text = newPlayer.name;
    }
}
