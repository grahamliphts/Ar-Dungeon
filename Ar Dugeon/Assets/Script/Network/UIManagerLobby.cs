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
    public string ModelName;
    public string FieldName;
    public Dropdown ModelDrop;
    public Dropdown FieldDrop;
    void Start ()
    {
        networkScript = GetComponent<NetworkManager>();
        ModelName = ModelDrop.captionText.text;
        FieldName = FieldDrop.captionText.text;
    }
	
    public void OnModelChange()
    {
        ModelName = ModelDrop.captionText.text;
       
    }

    public void OnFieldChange()
    {
        FieldName = FieldDrop.captionText.text;
    }

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
