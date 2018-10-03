using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MessageInter : MonoBehaviour {

    // Attributs
    public Text TextInter;    //Méthodes
	void Start ()
    {
        TextInter = GetComponent<Text>();    //Initialise le texte
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void addText (string txt)
    {
        TextInter.text = txt;
    }

    void removeText()
    {
        TextInter.text = "";
    }

}
