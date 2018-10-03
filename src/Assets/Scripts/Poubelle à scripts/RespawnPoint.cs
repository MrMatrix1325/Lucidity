using UnityEngine;
using System.Collections;

public class RespawnPoint : MonoBehaviour {

    // Use this for initialization
    public CheckpointsManager chkmngr;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Joueur")
        {
            chkmngr.Respawn();
        }
    }
}
