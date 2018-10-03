using UnityEngine;
using System.Collections;

public class Autop : MonoBehaviour
{


    public Vector3 debut;
    public Vector3 fin = new Vector3(0, 0, 10);
    public GameObject joueur;
    public GameObject[] listJoueur;
    public bool aller = true;
    public float coef;
    public float temps;
    public PhotonView photonView;
    public bool debug = false;

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Clips")
        {
            coll.transform.parent = transform;
            coll.transform.eulerAngles = new Vector3(0, 0, 0);
            coll.transform.localScale = new Vector3(1 / transform.lossyScale.x, 1 / transform.lossyScale.y, 1 / transform.lossyScale.z);
        }
    }

    public void moveIt()
    {
        if (!Globals.isMulti)
        {
            if (Mathf.Abs(joueur.transform.position.x - transform.position.x) <= transform.lossyScale.x / 2 && Mathf.Abs(joueur.transform.position.y - transform.position.y) < 2.5 && Mathf.Abs(joueur.transform.position.z - transform.position.z) <= transform.lossyScale.z / 2)
            {
                joueur.transform.SetParent(transform);
            }
            else if (joueur.transform.parent == transform)
            {
                joueur.transform.parent = null;
            }
            if (aller)
            {
                transform.Translate((fin - debut) / 5 * temps * coef);
            }
            else
            {
                transform.Translate((debut - fin) / 5 * temps * coef);
            }
        }
        else
        {
            foreach (GameObject go in listJoueur)
            {
                if (Mathf.Abs(go.transform.position.x - transform.position.x) <= transform.lossyScale.x / 2 && Mathf.Abs(go.transform.position.y - transform.position.y) < 2.5 && Mathf.Abs(go.transform.position.z - transform.position.z) <= transform.lossyScale.z / 2)
                {
                    go.transform.SetParent(transform);
                }
                else if (go.transform.parent == transform)
                {
                    go.transform.parent = null;
                }
            }
            if (PhotonNetwork.isMasterClient)
            {
                /*if (aller)
                {
                    transform.Translate(fin - debut / 5 * temps * coef);
                }
                else
                {
                    transform.Translate(debut - fin / 5 * temps * coef);
                }
                if (NetworkController.connected)
                {
                    photonView.RPC("mooveMe", PhotonTargets.All, transform.position);
                }*/

                if (aller)
                {
                    transform.Translate((Mathf.Abs(fin.x) - Mathf.Abs(debut.x)) / 5 * temps * coef, (Mathf.Abs(fin.y) - Mathf.Abs(debut.y)) / 5 * temps * coef, (Mathf.Abs(fin.z) - Mathf.Abs(debut.z)) / 5 * temps * coef);
                }
                else
                {
                    transform.Translate((Mathf.Abs(debut.x) - Mathf.Abs(fin.x)) / 5 * temps * coef, (Mathf.Abs(debut.y) - Mathf.Abs(fin.y)) / 5 * temps * coef, (Mathf.Abs(debut.z) - Mathf.Abs(fin.z)) / 5 * temps * coef);
                }
                if (NetworkController.connected)
                {
                    photonView.RPC("mooveMe", PhotonTargets.All, transform.position);
                }
            }
        }
    }

    [PunRPC]
    public void mooveMe(Vector3 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting && PhotonNetwork.isMasterClient)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
        }
        else
        {
            // Network player, receive data
            transform.position = Vector3.Lerp(transform.position, (Vector3)stream.ReceiveNext(), Time.deltaTime * 5);
        }
    }

    public void testIt()
    {
        if (transform.position.x > fin.x || transform.position.y > fin.y || transform.position.z > fin.z)
        {
            aller = false;
            if (debug)
                Debug.Log(transform.position);
        }
        else if (transform.position.x < debut.x || transform.position.y < debut.y || transform.position.z < debut.z)
        {
            aller = true;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (!Globals.isMulti)
        {
            //debut = transform.position;
            joueur = GameObject.FindWithTag("Joueur");
        }
        else
        {
            //debut = transform.position;
        }
        photonView = GetComponent<PhotonView>();
    }

    public void refreshList()
    {
        if (Globals.isMulti)
        {
            listJoueur = Globals.listJoueur;
        }
    }

    // Update is called once per frame
    void Update()
    {
        temps = Time.deltaTime;
        refreshList();
        testIt();
        moveIt();
    }
}
