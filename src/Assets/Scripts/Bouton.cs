    using UnityEngine;
using System.Collections;

public class Bouton : MonoBehaviour
{

    public HideGroup group;
    public bool push = false;
    public float coeff = 100;
    public float value = 0;
    public Stuff joueur;
    public GameObject[] listJoueur;
    public PhotonView photonView;
    bool sound = false;



    void OnTriggerStay(Collider other)
    {
        if (!Globals.isMulti)
        {
            if (!push && other.tag == "Joueur" && joueur.push_button)
            {
                value += coeff;
            }
        }
        else
        {
            int len = Globals.listJoueur.Length;
            for (int i = 0; i < len; i++)
            {
                if (!push && other.tag == "Joueur" && Globals.listJoueur[i].GetComponent<Stuff>().push_button)
                {
                    value += coeff;
                }
            }
        }
    }


    public void pushingIt()
    {
        if (!Globals.isMulti)
        {
            if (value >= 100)
            {
                push = true;
                value = 100;
                group.notHide();
            }
            else
            {

                push = false;
            }
        }
        else
        {
            if (value >= 100)
            {
                push = true;
                value = 100;
                if (!sound)
                {
                    GameObject.Find("Bruitage").SendMessage("play", "Button");
                    sound = true;
                }
                group.notHide();
            }
            else
            {
                push = false;
                sound = false;
            }
            if (NetworkController.connected)
            {
                photonView.RPC("pushMe", PhotonTargets.All, push);
            }
        }
    }

    [PunRPC]
    void pushMe(bool push)
    {
        this.push = push;
        if (push)
        {
            value = 100;
            group.notHide();
        }
        else
        {

        }
    }
    public void refreshList()
    {
        if (Globals.isMulti)
        {
            listJoueur = Globals.listJoueur;
        }
    }

    // Use this for initialization
    void Start()
    {
        joueur = GameObject.FindWithTag("Joueur").GetComponent<Stuff>();
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        refreshList();
        pushingIt();
    }
}