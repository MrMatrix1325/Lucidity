using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if (!Globals.Paused && Input.GetButtonDown("Interaction"))
        {
            SendMessage("play", "Main");
        }

        
       
        
	}
}
