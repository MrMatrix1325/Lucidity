using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Objectifs : MonoBehaviour {

    public Text txt;
    public string str = "";
    public bool first = false;
    public int i = 1;

    // Use this for initialization
    void Start ()
    {
        str = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void print (string obj)
    {
        if (! first)
        {
            str += i + " - " + obj + "."  ;
            txt.text =  "Objectif(s) : " + str;
            first = true;
            i++;
        }

    }
    void isCompleted()
    {
        str += "(OK) \n";
        str += "                  ";
        first = false;
        txt.text = "Objectif(s) :" + str;
        GameObject.Find("Bruitage").SendMessage("play", "SNCF");
    }

    void replace(string rep)
    {
        str = +(i - 1) + " - " + rep;
        txt.text = "Objectif(s) : "  + str;
    }

}

