using UnityEngine;
using System.Collections;

public class Door2_Open : MonoBehaviour {

    public Porte door;
    public bool msg;
	// Use this for initialization
	void Start () {
        msg = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (!door.open)
        {
            msg = true;
            GameObject.Find("TextInter").SendMessage("addText", "Pour cela tu vas avoir besoin d'une ressource essentiel tout au long de ton périple ");
        }
        else if (msg)
        {
            msg = false;
            GameObject.Find("TextInter").SendMessage("removeText");
        }
    }
}

