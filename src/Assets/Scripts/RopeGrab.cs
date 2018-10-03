using UnityEngine;
using System.Collections;

public class RopeGrab : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
	player = GameObject.FindGameObjectWithTag("Joueur");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Corde" && Input.GetButton("Fire1"))
        {
            Debug.Log("Accroché");
            //gameObject.transform.position = other.gameObject.transform.position + new Vector3(Mathf.Cos(Mathf.PI/180 * player.transform.rotation.y), 0, Mathf.Sin(Mathf.PI/180 * player.transform.rotation.y));
            gameObject.transform.position = other.gameObject.transform.position + new Vector3(1,0,0);
        }
    }

}
