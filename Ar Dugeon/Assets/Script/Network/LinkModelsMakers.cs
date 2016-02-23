using UnityEngine;
using System.Collections;

public class LinkModelsMakers : MonoBehaviour
{
    NetworkManager networkManager;
    string modelName;
    string fieldName;
    bool find;

    void Start ()
    {
        modelName = "";
        GameObject manager = GameObject.Find("NetworkManager");
        if(!PhotonNetwork.isMasterClient)
        {
            networkManager = manager.GetComponent<NetworkManager>();
            string playerName = networkManager.playerData[0];
            modelName = networkManager.playerData[1];
            networkManager.photonView.RPC("LoadModelMarker", PhotonTargets.MasterClient, playerName, modelName);
        }

        else
        {
            networkManager = manager.GetComponent<NetworkManager>();
            fieldName = networkManager.playerData[1];
            networkManager.photonView.RPC("LoadField", PhotonTargets.All, fieldName);
        }
    }
	
	void Update ()
    {

	}
}
