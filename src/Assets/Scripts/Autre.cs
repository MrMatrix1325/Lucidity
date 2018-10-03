using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Autre : Photon.MonoBehaviour
{

    public Vector3 last;
    public Vector3 newv;
    public float basic = 10f;
    public bool attraction = false;
    public Vector3 rot;
    public bool stop = false;
    public bool destroyIt = false;
    public float temps;
    public float timer = 5f;
    public GameObject coll;
    public GameObject joueur;
    public List<GameObject> liste = new List<GameObject>();
    public Vector3 trans;
    public Vector3 correctPlayerPos;
    public Vector3 rott;
    public GameObject[] listJoueur;
    public bool done;
    public bool travers = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portail")
        {
            travers = true;
        }
        if (!stop && !travers && other.tag != "Joueur" && other.tag != "Tête" && other.tag != "Porte" && other.tag != "Source")
        {
            timer = 5f;
            basic = 0;
            stop = true;
            attraction = other.tag == "Mur";
            coll = other.gameObject;
            if (other.tag == "Plateforme")
            {
                gameObject.transform.rotation = coll.transform.rotation;
                foreach (Autre collider in GameObject.FindObjectsOfType<Autre>())
                {
                    if (collider.stop && collider.attraction)
                    {
                        rot = collider.GetComponent<Autre>().rot;
                        liste.Add(collider.gameObject);
                    }
                }
            }
            else if (other.tag == "Mur")
            {
                rot = coll.transform.eulerAngles;
                gameObject.transform.rotation = coll.transform.rotation;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Portail")
        {
            travers = false;
        }
    }

    public void isDestroy()
    {
        if (!Globals.isMulti)
        {
            last = newv;
            newv = transform.position;
            timer -= temps;
            if (timer < 0)
            {
                destroyIt = true;
            }
            if (destroyIt)
            {
                joueur.transform.parent = null;
                joueur.transform.GetComponent<Stuff>().onAutre = true;
                Destroy(gameObject);
            }
        }
        else
        {
            if (PhotonNetwork.isMasterClient)
            {
                last = newv;
                newv = transform.position;
                timer -= temps;
                if (timer < 0)
                {
                    destroyIt = true;
                }
                if (destroyIt)
                {
                    joueur.transform.parent = null;
                    joueur.transform.GetComponent<Stuff>().onAutre = true;
                    photonView.RPC("DestroyClip", PhotonTargets.All, gameObject.GetComponent<PhotonView>().viewID);
                }
            }
        }
    }

    [PunRPC]
    public void DestroyClip(int ID)
    {
        GameObject goo = gameObject;
        foreach (PhotonView go in GameObject.FindObjectsOfType<PhotonView>())
        {
            if (go.viewID == ID)
            {
                goo = go.gameObject;
            }
        }
        Destroy(goo);
    }


    public void Moove()
    {
        if (!Globals.isMulti)
        {
            if (coll.tag == "Plateforme")
            {
                transform.parent = coll.transform;
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.localScale = new Vector3(1 / coll.transform.lossyScale.x, 1 / coll.transform.lossyScale.y, 1 / coll.transform.lossyScale.z);
                if (Mathf.Abs(joueur.transform.position.x - coll.transform.position.x) <= coll.transform.lossyScale.x && Mathf.Abs(joueur.transform.position.y - coll.transform.position.y) < 2.5 && Mathf.Abs(joueur.transform.position.z - coll.transform.position.z) <= coll.transform.lossyScale.z)
                {
                    joueur.transform.parent = coll.transform;
                }
                else if (joueur.transform.parent == coll.transform)
                {
                    joueur.transform.parent = null;
                }
                foreach (GameObject go in liste)
                {
                    Vector3 vect3 = go.transform.position - coll.transform.position;
                    /*Vector3 rot = new Vector3(0, (Mathf.Asin((vect3.z) / Mathf.Sqrt(vect3.x * vect3.x + vect3.z * vect3.z)) > 0 ? Mathf.Acos((vect3.x) / Mathf.Sqrt(vect3.x * vect3.x + vect3.z * vect3.z)) : -Mathf.Acos((vect3.x) / Mathf.Sqrt(vect3.x * vect3.x + vect3.z * vect3.z))) * 180 / Mathf.PI, 0);
                    Vector3 go_rot = new Vector3(0, -(go.transform.localEulerAngles.y > 180 ? (go.transform.localEulerAngles.y % 180) - 180 : go.transform.localEulerAngles.y), 0);
                    Debug.Log("Go Rot: " + go_rot);
                    Vector3 rot_res = rot - go_rot;
                    rot_res.y -= 90;*/
                    //Debug.Log("Rot Res: " + rot_res);
                    trans = new Vector3(vect3.x, 0, vect3.z);
                    if (vect3.x < 0)
                    {
                        trans.x += coll.transform.localScale.x / 2;
                    }
                    else
                    {
                        trans.x -= coll.transform.localScale.x / 2;
                    }
                    if (vect3.z < 0)
                    {
                        trans.z += coll.transform.localScale.z / 2;
                    }
                    else
                    {
                        trans.z -= coll.transform.localScale.z / 2;
                    }
                    coll.transform.Translate(trans * temps);
                    if (newv == last)
                    {
                        Destroy(go);
                        destroyIt = true;
                    }
                }
            }
        }
        else
        {
            if (coll.tag == "Plateforme")
            {
                foreach (GameObject goo in listJoueur)
                {
                    transform.parent = coll.transform;
                    transform.localScale = new Vector3(1 / coll.transform.lossyScale.x, 1 / coll.transform.lossyScale.y, 1 / coll.transform.lossyScale.z);
                    if (Mathf.Abs(goo.transform.position.x - coll.transform.position.x) <= coll.transform.lossyScale.x && Mathf.Abs(goo.transform.position.y - coll.transform.position.y) < 2.5 && Mathf.Abs(goo.transform.position.z - coll.transform.position.z) <= coll.transform.lossyScale.z)
                    {
                        goo.transform.parent = coll.transform;
                    }
                    else if (goo.transform.parent == coll.transform)
                    {
                        goo.transform.parent = null;
                    }
                    foreach (GameObject go in liste)
                    {
                        if (go.name == gameObject.name)
                        {
                            Vector3 vect3 = go.transform.position - coll.transform.position;
                            /*Vector3 rot = new Vector3(0, (Mathf.Asin((vect3.z) / Mathf.Sqrt(vect3.x * vect3.x + vect3.z * vect3.z)) > 0 ? Mathf.Acos((vect3.x) / Mathf.Sqrt(vect3.x * vect3.x + vect3.z * vect3.z)) : -Mathf.Acos((vect3.x) / Mathf.Sqrt(vect3.x * vect3.x + vect3.z * vect3.z))) * 180 / Mathf.PI, 0);
                            Vector3 go_rot = new Vector3(0, -(go.transform.localEulerAngles.y > 180 ? (go.transform.localEulerAngles.y % 180) - 180 : go.transform.localEulerAngles.y), 0);
                            Debug.Log("Go Rot: " + go_rot);
                            Vector3 rot_res = rot - go_rot;
                            rot_res.y -= 90;*/
                            //Debug.Log("Rot Res: " + rot_res);
                            trans = new Vector3(vect3.x, 0, vect3.z);
                            if (vect3.x < 0)
                            {
                                trans.x += coll.transform.localScale.x / 2;
                            }
                            else
                            {
                                trans.x -= coll.transform.localScale.x / 2;
                            }
                            if (vect3.z < 0)
                            {
                                trans.z += coll.transform.localScale.z / 2;
                            }
                            else
                            {
                                trans.z -= coll.transform.localScale.z / 2;
                            }
                            /*if ((rot_res.y) < 90 && (rot_res.y) > -90)
                            {
                                Debug.Log("Un");
                                Debug.Log("Cos: " + Mathf.Cos(go_rot.y * Mathf.PI / 180));
                                trans -= new Vector3(coll.transform.localScale.x / 2, 0, 0);
                            }
                            else if (rot_res.y < 180 && rot_res.y > -180)
                            {
                                Debug.Log("Deux");
                                Debug.Log("Cos: " + Mathf.Cos(go_rot.y * Mathf.PI / 180));
                                trans += new Vector3(coll.transform.localScale.x / 2, 0, 0);
                            }
                            else
                            {
                                Debug.Log("Trois");
                                Debug.Log("Cos: " + Mathf.Cos(go_rot.y * Mathf.PI / 180));
                            }*/
                            /*if (trans.x < trans.z)
                            {
                                trans.x = vect3.x;
                            }*/
                            /*rot_res.y += 90;
                            if ((rot_res.y) < 90 && (rot_res.y) > -90)
                            {
                                Debug.Log("Un");
                                Debug.Log("Cos: " + Mathf.Cos(go_rot.y * Mathf.PI / 180));
                                trans -= new Vector3(0, 0, go.GetComponent<Autre>().coll.transform.localScale.z / 2);
                            }
                            else
                            {
                                Debug.Log("Deux");
                                Debug.Log("Cos: " + Mathf.Cos(go_rot.y * Mathf.PI / 180));
                                trans += new Vector3(0, 0, go.GetComponent<Autre>().coll.transform.localScale.z / 2);
                            }*/
                            coll.transform.Translate(trans * temps);
                            if (PhotonNetwork.isMasterClient)
                            {
                                if (newv == last)
                                {
                                    go.GetComponent<Autre>().destroyIt = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public Quaternion rotateIf(Quaternion clipGO)
    {
        if (!done)
        {
            return clipGO;
        }
        else
        {
            return coll.transform.rotation;
        }
    }

    public void testIt()
    {
        if (NetworkController.connected)
        {
            photonView.RPC("mooveMe", PhotonTargets.All, transform.position);
        }
    }

    [PunRPC]
    public void mooveMe(Vector3 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5);
    }


    // Use this for initialization

    void Start()
    {
        done = false;
        newv = transform.position;
        last = new Vector3(0, 0, 0);
        correctPlayerPos = transform.position;
        joueur = GameObject.FindWithTag("Joueur");
        GetComponent<PhotonView>().ObservedComponents.Add(GetComponent<Autre>());
    }

    // Update is called once per frame

    void Update()
    {
        if (!Globals.isMulti)
        {
            temps = Time.deltaTime;
            gameObject.transform.Translate(new Vector3(0, 0, basic * temps));
            try
            {
                Moove();
            }
            catch
            {

            }
            isDestroy();
        }
        else
        {
            temps = Time.deltaTime;
            listJoueur = Globals.listJoueur;
            gameObject.transform.Translate(new Vector3(0, 0, basic * temps));
            testIt();
            try
            {
                Moove();
            }
            catch
            {

            }
            isDestroy();
        }
    }
}