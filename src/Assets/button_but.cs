using UnityEngine;
using System.Collections;

public class button_but : MonoBehaviour {


    public Bouton button3;
    public Bouton button4;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

        if (!button3.push && !button4.push)
        {
            GameObject.Find("Chat").SendMessage("SendMessage", "Activer les plateformes (reste 2 sur 2)");
        }
        else if (button3.push && !button4.push && !button3.push && button4.push)
        {
            GameObject.Find("Chat").SendMessage("SendMessage", "Activer les plateformes (reste 1 sur 2)");
        }
        else if (button3.push && button4.push)
        {
            GameObject.Find("Chat").SendMessage("SendMessage", "Activer les plateformes (reste 0 sur 2)");
        }
        else
        {
            GameObject.Find("Chat").SendMessage("SendMessage", "Activer les plateformes (reste 2 sur 2)");
        }

    }
}
