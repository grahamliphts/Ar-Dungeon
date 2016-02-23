using UnityEngine;
using System.Collections;

public class LinkModelsMakers : MonoBehaviour
{
    NetworkManager networkManager;
    string modelName;
    bool find;

    void Start ()
    {
        modelName = "";
        GameObject manager = GameObject.Find("NetworkManager");
        if(!PhotonNetwork.isMasterClient)
        {
            networkManager = manager.GetComponent<NetworkManager>();
            string playerName = networkManager.data[0];
            modelName = networkManager.data[1];

            networkManager.photonView.RPC("LoadModelMarker", PhotonTargets.MasterClient, playerName, modelName);
        }
	}
	
	void Update ()
    {

	}
}
