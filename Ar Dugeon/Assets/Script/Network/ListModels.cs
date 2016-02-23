using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListModels : MonoBehaviour
{
    public Transform panel;
    public List<string> modelsData;
    private List<GameObject> modelsList;
    private GameObject selectedObject;
    private Color unselectedColor;
    public string selectedModelName;

    public void OnEnable()
    {
        if (modelsList == null)
        {
            modelsList = new List<GameObject>();
            unselectedColor = new Color(171 / 255.0f, 174 / 255.0f, 182 / 255.0f, 1);
        }
    }

    void Start()
    {
        int i = 0;
        if (null != modelsData)
        {
            for (i = 0; i < modelsData.Count; i++)
            {
                GameObject button = (GameObject)Instantiate(Resources.Load("SampleButton"));
                modelsList.Add(button);
                button.SetActive(true);
                button.transform.SetParent(panel, false);
                button.transform.FindChild("Text").GetComponent<Text>().text = modelsData[i];
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
        int selected = modelsList.IndexOf(selectedObject);
        if (selected >= 0 && selected < modelsList.Count)
        {
            selectedObject = modelsList[selected];
            selectedObject.GetComponent<Image>().color = Color.green;
            selectedModelName = modelsData[selected];
        }
    }
}
