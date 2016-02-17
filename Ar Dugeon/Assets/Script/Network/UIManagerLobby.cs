using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManagerLobby : MonoBehaviour
{
    private NetworkManager networkScript;
    public GameObject connectionPanel;
    public GameObject waitPanel;

    public GameObject masterPanel;
    public Text NbPlayers;
	// Use this for initialization
	void Start ()
    {
        networkScript = GetComponent<NetworkManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        int nbPlayers = PhotonNetwork.playerList.Length;
        NbPlayers.text = nbPlayers + "/4";
        if (networkScript.inRoom)
        {
            connectionPanel.SetActive(false);
            waitPanel.SetActive(true);
        }
        else
        {
            connectionPanel.SetActive(true);
            waitPanel.SetActive(false);
        }

        if (PhotonNetwork.isMasterClient)
            masterPanel.SetActive(true);
        else 
            masterPanel.SetActive(false);
    }
}
