using UnityEngine;
using System.Collections;

public class SetParenting : MonoBehaviour
{
    /*public string nameMarker = "";
    bool find = false;

	void Update ()
    {
	    if(nameMarker != "" && !find)
        {
            GameObject sceneRoot = GameObject.Find("Scene root");
            Transform marker = sceneRoot.transform.Find(nameMarker);
            this.transform.SetParent(marker, false);
            find = true;
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
                stream.SendNext(nameMarker);
        }
        else
        {
            nameMarker = (string)stream.ReceiveNext();
        }
    }*/
}
