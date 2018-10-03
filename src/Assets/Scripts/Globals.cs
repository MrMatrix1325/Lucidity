using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {


    public static bool isMulti;
    public static GameObject[] listJoueur;
    public static bool Paused;
    public static GameObject MainPlayer;
    public static string PlayerName;
    public static int weapon_id = 0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //OUCH CA FAIT MAL
        if (isMulti)
        {
            listJoueur = GameObject.FindGameObjectsWithTag("Joueur");
        }
    }
}
