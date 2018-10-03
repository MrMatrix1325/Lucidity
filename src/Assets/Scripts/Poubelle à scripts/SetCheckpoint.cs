using UnityEngine;
using System.Collections;

public class SetCheckpoint : MonoBehaviour {

    public CheckpointsManager chkmngr;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Joueur" && chkmngr.CurrentCheckpoint != gameObject)
        {
            Debug.Log("Checkpoint Set !");
            chkmngr.CurrentCheckpoint = gameObject;
        }
    }
}
