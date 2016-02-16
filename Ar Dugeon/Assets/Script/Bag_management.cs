using UnityEngine;
using System.Collections;

public class Bag_management : MonoBehaviour {

    [SerializeField]
    private UnityEngine.UI.Text m_BagContent;
    [SerializeField]
    private UnityEngine.UI.Text m_AddContent;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    public void updateContent(string val)
    {
        Debug.Log("add content");
        m_BagContent.text = m_BagContent.text + "\n - " + m_AddContent.text;
        m_AddContent.text = "";
    }


}
