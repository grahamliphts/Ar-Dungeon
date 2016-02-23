using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListPlayers : MonoBehaviour
{
    public Transform panel;
    private List<GameObject> playerList;

    public void OnEnable()
    {
        if (playerList == null)
            playerList = new List<GameObject>();
        InvokeRepeating("PopulateNameList", 0, 2);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    public void PopulateNameList()
    {
        if(PhotonNetwork.isMasterClient)
        {
            int i = 0;
            PhotonPlayer[] players = PhotonNetwork.playerList;

            for (int j = 0; j < playerList.Count; j++)
            {
                Destroy(playerList[j]);
            }
            playerList.Clear();

            for (i = 0; i < players.Length; i++)
            {
                if (players[i].name != PhotonNetwork.playerName)
                {
                    GameObject gameobject = (GameObject)Instantiate(Resources.Load("PlayerObject"));
                    playerList.Add(gameobject);
                    gameobject.transform.SetParent(panel, false);
                    gameobject.GetComponentInChildren<Text>().text = players[i].name;
                }
            }
        }
    }
}
