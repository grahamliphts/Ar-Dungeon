using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListMap : MonoBehaviour
{
    public Transform panel;
    public List<string> mapData;
    private List<GameObject> mapList;
    private GameObject selectedObject;
    private Color unselectedColor;
    public string selectedMapname;

    public void OnEnable()
    {
        if (mapList == null)
        {
            mapList = new List<GameObject>();
            unselectedColor = new Color(171 / 255.0f, 174 / 255.0f, 182 / 255.0f, 1);
        }
    }

    void Start()
    {
        int i = 0;
        if (null != mapData)
        {
            for (i = 0; i < mapData.Count; i++)
            {
                GameObject button = (GameObject)Instantiate(Resources.Load("SampleButton"));
                mapList.Add(button);
                button.SetActive(true);
                button.transform.SetParent(panel, false);
                button.transform.FindChild("Text").GetComponent<Text>().text = mapData[i];
            }
        }
    }
    public void OnDisable()
    {
        CancelInvoke();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject map = EventSystem.current.currentSelectedGameObject;
            if (map != null)
            {
                if (map.name == "SampleButton(Clone)")
                {
                    if (selectedObject != null)
                        selectedObject.GetComponent<Image>().color = unselectedColor;

                    selectedObject = map;
                    selectedObject.GetComponent<Image>().color = Color.green;
                }
            }
        }
        int selected = mapList.IndexOf(selectedObject);
        if (selected >= 0 && selected < mapList.Count)
        {
            selectedObject = mapList[selected];
            selectedObject.GetComponent<Image>().color = Color.green;
            selectedMapname = mapData[selected];
        }
    }
}
