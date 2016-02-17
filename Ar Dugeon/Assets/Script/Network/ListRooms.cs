using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListRooms : MonoBehaviour
{
    public Transform panel;
    private List<GameObject> serverList;
    private GameObject selectedObject;
    private Color unselectedColor;
    public string selectedRoomName;

    public void OnEnable()
    {
        if (serverList == null)
        {
            serverList = new List<GameObject>();
            unselectedColor = new Color(171 / 255.0f, 174 / 255.0f, 182 / 255.0f, 1);
        }
        InvokeRepeating("PopulateServerList", 0, 2);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject server = EventSystem.current.currentSelectedGameObject;
            if (server != null)
            {
                if (server.name == "ServerButton(Clone)")
                {
                    if (selectedObject != null)
                        selectedObject.GetComponent<Image>().color = unselectedColor;

                    selectedObject = server;
                    selectedObject.GetComponent<Image>().color = Color.green;
                }
            }
        }
    }

    public void PopulateServerList()
    {
        int i = 0;
        RoomInfo[] hostData = PhotonNetwork.GetRoomList();

        int selected = serverList.IndexOf(selectedObject);

        for (int j = 0; j < serverList.Count; j++)
        {
            Destroy(serverList[j]);
        }
        serverList.Clear();

        if (null != hostData)
        {
            for (i = 0; i < hostData.Length; i++)
            {
                if (!hostData[i].open)
                    continue;

                GameObject button = (GameObject)Instantiate(Resources.Load("ServerButton"));
                serverList.Add(button);
                button.SetActive(true);
                button.transform.SetParent(panel, false);
                button.transform.FindChild("ServerText").GetComponent<Text>().text = hostData[i].name;
                button.transform.FindChild("PlayerText").GetComponent<Text>().text = hostData[i].playerCount + "/" + hostData[i].maxPlayers;
            }
        }
      
        if (selected >= 0 && selected < serverList.Count)
        {
            selectedObject = serverList[selected];
            selectedRoomName = hostData[selected].name;
            selectedObject.GetComponent<Image>().color = Color.green;
        }
    }

}
