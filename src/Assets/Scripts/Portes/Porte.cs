using UnityEngine;
using System.Collections;

public class Porte : MonoBehaviour
{

    //Attributs de la classe :
    public float temps;
    public bool open = false;
    public float un;
    public float coef = 100;
    public float value = 0;
    public Stuff Joueur;
    public GameObject[] listJoueur;
    public GameObject Bruitage;
    public float rot;
    public PhotonView photonView;
    public bool msg = true;
    public bool sound = false;

    //Méthodes

    void OnTriggerStay(Collider other)
    {
        if (!Globals.isMulti)
        {
            if (!open && other.tag == "Joueur" && Joueur.open_door)
            {
                value += coef;
            }
        }
        else
        {
            int len = listJoueur.Length;
            for (int i = 0; i < len; i++)
            {
                if (!open && other.tag == "Joueur" && listJoueur[i].GetComponent<Stuff>().open_door)
                {
                    value += coef;
                }
            }
        }
    }
    public void Opening()
    {
        if (!Globals.isMulti)
        {
            if (value >= 100)
            {
                open = true;
                value = 100;
                if (!sound)
                {
                    sound = true;
                    Bruitage.SendMessage("play", "door");
                }
                transform.localEulerAngles = new Vector3(0, un, 0);
                if (msg)
                {
                    msg = false;

                }
            }
            else
            {
                sound = false;
                open = false;
                transform.localEulerAngles = new Vector3(0, un - 90, 0);
            }
        }
        else
        {
            if (value >= 100)
            {
                open = true;
                value = 100;
                transform.localEulerAngles = new Vector3(0, un, 0);
            }
            else
            {
                sound = false;
                open = false;
                transform.localEulerAngles = new Vector3(0, un - 90, 0);
            }
            if (NetworkController.connected)
            {
                photonView.RPC("rotateMe", PhotonTargets.All, open);
            }
        }
    }

    [PunRPC]
    void rotateMe(bool open)
    {
        this.open = open;
        if (open)
        {
            value = 100;
            transform.localEulerAngles = new Vector3(0, un, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, un - 90, 0);
        }
    }

    public void refreshList()
    {
        if (Globals.isMulti)
        {
            listJoueur = GameObject.FindGameObjectsWithTag("Joueur");
        }
    }

    // Use this for initialization  
    void Start()
    {
        un = transform.localEulerAngles.y + 90;
        Joueur = GameObject.FindObjectOfType<Stuff>();
        photonView = GetComponent<PhotonView>();
        sound = false;
        Bruitage = GameObject.Find("Bruitage");
    }

    // Update is called once per frame
    void Update()
    {
        temps = Time.deltaTime;
        refreshList();
        Opening();
    }

}
