using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointsManager : MonoBehaviour
{

    public GameObject CurrentCheckpoint;
    public GameObject Player;
    public GameObject AudioManager;
    public OptionsMenuScript Volume;
    public bool found = false;

    // Use this for initialization
    public void Start()
    {
        CurrentCheckpoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        //Si multi chercher le joueur principal, sinon trouver "Corps"
        if (Globals.isMulti)
        {
            GameObject[] Players = GameObject.FindGameObjectsWithTag("Joueur");
            bool found = false;
            for (int i = 0; i < Players.Length && !found; i++)
                if (Players[i].GetComponent<PhotonView>().isMine)
                {
                    found = true;
                    Player = Players[i];
                    //Debug.Log("Trouvé " + Players[i].GetComponent<PhotonView>().viewID);
                }

        }
        else
            Player = GameObject.Find("Corps");
        Respawn();
        AudioManager = GameObject.Find("AudioManager");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Respawn()
    {
        
        Player.transform.position = CurrentCheckpoint.transform.position;
        Player.transform.Translate(new Vector3(0, 0, 0));
        
        Player.GetComponent<Stuff>().lucidity_stock = Player.GetComponent<Stuff>().lucidity_check;
        Player.transform.parent = null;
        foreach (GameObject porte in GameObject.FindGameObjectsWithTag("Porte"))
        {
            porte.GetComponent<Porte>().value = 0;
        }
        foreach (GameObject clips in GameObject.FindGameObjectsWithTag("Plateforme"))
        {
            clips.GetComponent<PlateRI>().ResetIt();
        }
        foreach (GameObject clips in GameObject.FindGameObjectsWithTag("Sphere"))
        {
            clips.GetComponent<PointApparition>().resetIt();
        }
        GameObject.Find("AudioManager").SendMessage("play", "Die");
    }


    public void generate()
    {

    }
}
