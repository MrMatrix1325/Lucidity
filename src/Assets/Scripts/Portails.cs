using UnityEngine;
using System.Collections;

public class Portails : MonoBehaviour
{

    public string theTruth = null;
    public bool is_done = false;
    public bool stop = false;
    public bool is_finished = false;
    public float temps;
    public float basic = 10f;
    public PhotonView photonView;
    public GameObject Camera;
    public GameObject coll;
    public int joueur_ID;
    public GameObject joueur;
    public RenderTexture rt;
    public GameObject linked = null;
    public bool wait = false;
    public Vector3 begin;


    void OnTriggerEnter(Collider other)
    {
        if (!stop)
        {
            if (other.tag == "Mur")
            {
                coll = other.gameObject;
                stop = true;
            }
            else if (other.tag != "Joueur" && other.tag != "Tête" && other.tag != "Source")
            {
                destroy();
            }
        }
        if (is_done && (other.tag == "Lance" || other.tag == "Portail"))
        {
            other.gameObject.GetComponent<Portails>().destroy();
        }
        if (is_finished && !linked.GetComponent<Portails>().wait && (other.tag == "Joueur" || other.tag == "Clips"))
        {
            wait = true;
            other.gameObject.transform.position = linked.transform.position;
            other.gameObject.GetComponent<Deplacement>().Rotate_P(linked.transform.localEulerAngles - transform.localEulerAngles + new Vector3(0, 180, 0));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (linked != null && (other.tag == "Joueur" || other.tag == "Clips"))
        {
            linked.GetComponent<Portails>().wait = false;
        }
    }

    public void move()
    {
        if (Globals.isMulti)
        {
            if (!stop && PhotonNetwork.isMasterClient)
            {
                gameObject.transform.Translate(new Vector3(0, 0, basic * temps));
                photonView.RPC("mooveMe", PhotonTargets.All, transform.position);
            }
        }
        else
        {
            if (!stop)
            {
                gameObject.transform.Translate(new Vector3(0, 0, basic * temps));
            }
        }
    }

    [PunRPC]
    public void mooveMe(Vector3 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 10);
    }

    public void change()
    {
        if (Globals.isMulti)
        {
            if (!is_done && stop && PhotonNetwork.isMasterClient)
            {
                Vector3 pos = transform.position;
                Vector3 rot = transform.localEulerAngles;
                Vector3 rot_res = new Vector3();
                if (coll.transform.localScale.x < coll.transform.localScale.z)
                {
                    rot_res = new Vector3(Mathf.Abs(rot.z - coll.transform.localEulerAngles.z), Mathf.Abs(rot.y - coll.transform.localEulerAngles.y), Mathf.Abs(rot.x - coll.transform.localEulerAngles.x));
                }
                else
                {
                    rot_res = new Vector3(Mathf.Abs(rot.x - coll.transform.localEulerAngles.x), Mathf.Abs(rot.y - coll.transform.localEulerAngles.y), Mathf.Abs(rot.z - coll.transform.localEulerAngles.z));
                }
                if (rot_res.x < 90 || rot_res.x > 270)
                {
                    rot_res.x = 180 - coll.transform.localEulerAngles.x;
                }
                else
                {
                    rot_res.x = coll.transform.localEulerAngles.x - 180;
                }
                if (rot_res.y < 90 || rot_res.y > 270)
                {
                    rot_res.y = coll.transform.localEulerAngles.y;
                }
                else
                {
                    rot_res.y = coll.transform.localEulerAngles.y - 180;
                }
                if (rot_res.z < 90 || rot_res.z > 270)
                {
                    rot_res.z = 180 - coll.transform.localEulerAngles.z;
                }
                else
                {
                    rot_res.z = coll.transform.localEulerAngles.z - 180;
                }
                if (coll.transform.localScale.x < coll.transform.localScale.z)
                {
                    rot_res.y += 90;
                }
                joueur.GetComponent<Stuff>().instanciateMe(pos, rot_res, theTruth, "Portail", true, begin);
                destroy();
            }
        }
        else
        {
            if (!is_done && stop)
            {
                Vector3 pos = transform.position;
                Vector3 rot = transform.localEulerAngles;
                Vector3 rot_res = new Vector3();
                if (coll.transform.localScale.x < coll.transform.localScale.z)
                {
                    rot_res = new Vector3(Mathf.Abs(rot.z - coll.transform.localEulerAngles.z), Mathf.Abs(rot.y - coll.transform.localEulerAngles.y), Mathf.Abs(rot.x - coll.transform.localEulerAngles.x));
                }
                else
                {
                    rot_res = new Vector3(Mathf.Abs(rot.x - coll.transform.localEulerAngles.x), Mathf.Abs(rot.y - coll.transform.localEulerAngles.y), Mathf.Abs(rot.z - coll.transform.localEulerAngles.z));
                }
                if (rot_res.x < 90 || rot_res.x > 270)
                {
                    rot_res.x = 180 - coll.transform.localEulerAngles.x;
                }
                else
                {
                    rot_res.x = coll.transform.localEulerAngles.x - 180;
                }
                if (rot_res.y < 90 || rot_res.y > 270)
                {
                    rot_res.y = coll.transform.localEulerAngles.y;
                }
                else
                {
                    rot_res.y = coll.transform.localEulerAngles.y - 180;
                }
                if (rot_res.z < 90 || rot_res.z > 270)
                {
                    rot_res.z = 180 - coll.transform.localEulerAngles.z;
                }
                else
                {
                    rot_res.z = coll.transform.localEulerAngles.z - 180;
                }
                if (coll.transform.localScale.x < coll.transform.localScale.z)
                {
                    rot_res.y += 90;
                }
                joueur.GetComponent<Stuff>().instanciateMe(pos, rot_res, theTruth, "Portail", true, begin);
                Destroy(gameObject);
            }
        }
    }

    public void link()
    {
        if (is_done)
        {
            foreach (SelfCam sc in FindObjectsOfType<SelfCam>())
            {
                if (sc.joueur_ID == joueur_ID && sc.theTruth != theTruth)
                {
                    linked = sc.gameObject.transform.parent.gameObject;
                    if (theTruth == "l")
                    {
                        GetComponent<MeshRenderer>().material = Resources.Load("Materials/Right Render") as Material;
                    }
                    else
                    {
                        GetComponent<MeshRenderer>().material = Resources.Load("Materials/Left Render") as Material;
                    }
                    is_finished = true;
                }
            }
        }
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        if (Globals.isMulti)
        {
            if (!is_done)
            {
                begin = transform.localEulerAngles;
            }
            photonView = GetComponent<PhotonView>();
            foreach (Stuff go in FindObjectsOfType<Stuff>())
            {
                if (go.photonView.viewID == joueur_ID)
                {
                    joueur = go.gameObject;
                }
            }
        }
        else
        {
            if (!is_done)
            {
                begin = transform.localEulerAngles;
            }
            joueur = FindObjectOfType<Stuff>().gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        temps = Time.deltaTime;
        move();
        change();
        link();
    }
}
