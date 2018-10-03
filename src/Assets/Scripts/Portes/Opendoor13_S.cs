using UnityEngine;
using System.Collections;

public class Opendoor13_S : MonoBehaviour {

    public Porte door;
    public bool sound;
    public CheckpointCollision chm;


	// Use this for initialization
	void Start () {
        sound = true;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (door.open)
        {
            if(sound)
            {
 
                GameObject.Find("objectifs").SendMessage("isCompleted");
                GameObject.Find("TextInter").SendMessage("addText", "Tu vas maintenant utiliser une plateforme grise pour cela fait clic droit sur un mur et clic droit sur la plateforme");
                GameObject.Find("objectifs").SendMessage("print", "Aller à la fin du niveau");
                sound = false;
            }
        }
        else if (!chm.check)
        {
            sound = true ;
        }
	
	}
}
