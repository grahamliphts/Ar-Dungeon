using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManagerLobby : MonoBehaviour
{
    private NetworkManager networkScript;
    public GameObject connectionPanel;
    public GameObject waitPanel;

    public GameObject masterPanel;
    public GameObject clientPanel;
    public Text NbPlayers;
    public Text PlayerName;
    // Use this for initialization
    void Start ()
    {
        networkScript = GetComponent<NetworkManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        int maxPlayers = 0;
        int nbPlayers = PhotonNetwork.playerList.Length;
        if(PhotonNetwork.room != null)
            maxPlayers = PhotonNetwork.room.maxPlayers;
        NbPlayers.text = nbPlayers + "/" + maxPlayers.ToString();
        PlayerName.text = PhotonNetwork.playerName;
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
        {
            masterPanel.SetActive(true);
            clientPanel.SetActive(false);
        }
        else
        {
            masterPanel.SetActive(false);
            clientPanel.SetActive(true);
        }
    }
}
