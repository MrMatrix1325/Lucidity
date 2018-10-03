using UnityEngine;
using System.Collections;

public class PointApparition : MonoBehaviour {

    //Attributs 
    public GameObject Group;
    public GameObject point;
    public Stuff Player;
    public bool ispass;
    public bool done;

    // Use this for initialization
    void Start()
    {
        Group.SetActive(false);
        done = false;
        Player = GameObject.Find("Corps").GetComponent<Stuff>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (ispass)
        {
            Group.SetActive(true);
        }
        else
        {
            Group.SetActive(false);
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Joueur" && !done)
        {
            done = true;
            Player.GetComponent<Stuff>().lucidity_stock -= 10;
            Debug.Log("Plateforme apparue");
            GameObject.Find("Bruitage").SendMessage("play", "Insert");
            Group.SetActive(true);
        }
    }

    public void resetIt()
    {
        done = false;
    }

    /*void OnCollisionEnter(Collision col)
    {
        if (!enabled)
            return;
        if (col.gameObject.tag == "DieZone")
        {
            Group.SetActive(false);
        }
    }*/
    
    public void Reset()
    {
        Group.SetActive(false);
        Debug.Log("Reset");
    }
}
