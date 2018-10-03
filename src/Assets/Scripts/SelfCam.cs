using UnityEngine;
using System.Collections;

public class SelfCam : MonoBehaviour {

    public int joueur_ID;
    public string theTruth;

    public void setSide(string side)
    {
        theTruth = side;
    }

    public void setBuilder(int i)
    {
        joueur_ID = i;
    }

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
