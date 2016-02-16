using UnityEngine;
using System.Collections;

public class UI_Manager : MonoBehaviour
{

    [SerializeField]
    private GameObject m_StatView;
    [SerializeField]
    private GameObject m_SavesView;
    [SerializeField]
    private GameObject m_Bag_View;
    [SerializeField]
    private GameObject m_NotesView;
    [SerializeField]
    private GameObject m_Abilities_View;
    [SerializeField]
    private GameObject m_StuffView;
    // Use this for initialization
    void Start()
    {
        showView("stuff");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReceiveButton(string name)
    {
        Debug.Log("Button : " + name + " Received");
        showView(name);
    }

    public void showView(string name)
    {
        switch (name)
        {
            case "stuff":
                m_StatView.SetActive(false);
                m_SavesView.SetActive(false);
                m_Bag_View.SetActive(false);
                m_NotesView.SetActive(false);
                m_Abilities_View.SetActive(false);
                m_StuffView.SetActive(true);
                break;
            case "stats":
                m_StatView.SetActive(true);
                m_SavesView.SetActive(false);
                m_Bag_View.SetActive(false);
                m_NotesView.SetActive(false);
                m_Abilities_View.SetActive(false);
                m_StuffView.SetActive(false);
                break;
            case "bag":
                m_StatView.SetActive(false);
                m_SavesView.SetActive(false);
                m_Bag_View.SetActive(true);
                m_NotesView.SetActive(false);
                m_Abilities_View.SetActive(false);
                m_StuffView.SetActive(false);
                break;
            case "saves":
                m_StatView.SetActive(false);
                m_SavesView.SetActive(true);
                m_Bag_View.SetActive(false);
                m_NotesView.SetActive(false);
                m_Abilities_View.SetActive(false);
                m_StuffView.SetActive(false);
                break;
            case "note":
                m_StatView.SetActive(false);
                m_SavesView.SetActive(false);
                m_Bag_View.SetActive(false);
                m_NotesView.SetActive(true);
                m_Abilities_View.SetActive(false);
                m_StuffView.SetActive(false);
                break;
            case "abilities":
                m_StatView.SetActive(false);
                m_SavesView.SetActive(false);
                m_Bag_View.SetActive(false);
                m_NotesView.SetActive(false);
                m_Abilities_View.SetActive(true);
                m_StuffView.SetActive(false);
                break;
            case "AR":
                Debug.Log("Switch to AR mode");
                break;
            default:
                m_StatView.SetActive(false);
                m_SavesView.SetActive(false);
                m_Bag_View.SetActive(false);
                m_NotesView.SetActive(false);
                m_Abilities_View.SetActive(false);
                m_StuffView.SetActive(true);
                break;

        }
    }
}
