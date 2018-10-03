using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Stuff : MonoBehaviour
{


    //Attributs :

    private float temps;
    public float lucidity_stock = 100;
    public bool stock = true;
    public bool lucidity_able;
    public bool open_door = false;
    public bool onAutre = true;
    public float coef = 100;
    public GameObject Clip;
    public GameObject Lance;
    public GameObject Portail;
    public GameObject Camera;
    public GameObject Liane;
    public int i = 0;
    public bool godmod = false;
    public List<GameObject> portes;
    public float lucidity_check = 100;
    public bool input_F;
    public PhotonView photonView;
    public bool sound;
    public bool push_button = false;
    public float coefTir;
    public float coefPort;
    public Transmission transmission;
    public bool create_portail;
    public ChatHandler chatHandler;
    public GameObject chat;
    public Deplacement dep;


    //Méthodes :
    public void Get_Lucidity()
    {
        sound = false;
        if (lucidity_able && lucidity_stock < 100)
        {
            lucidity_stock += 20 * temps;
            if (!sound)
            {
                GameObject.Find("AudioManager").SendMessage("play", "Source");
                sound = true;
            }
        }

    }

    public void Use_Lucidity(Collider other)
    {
        if (stock)
        {
            if (!Globals.Paused && Input.GetButtonDown("Interaction"))
            {
                open_door = true;
                push_button = true;
                lucidity_stock -= other.gameObject.GetComponent<Porte>().coef / 100;
            }
            else
            {
                open_door = false;
                push_button = false;
            }
        }
        else
        {
            open_door = false;
            push_button = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Source")
        {
            lucidity_able = true;
        }
        if (other.tag == "Clips")
        {
            onAutre = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Porte" && !other.gameObject.GetComponent<Porte>().open || other.tag == "Bouton" && !other.gameObject.GetComponent<Bouton>().push)
        {
            Use_Lucidity(other);
        }
        if (other.tag == "Clips")
        {
            onAutre = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Porte")
        {
            open_door = false;
        }
        if (other.tag == "Source")
        {
            lucidity_able = false;
        }
        if (other.tag == "Clips")
        {
            onAutre = true;
        }
        if (other.tag == "Bouton")
        {
            push_button = false;
        }
    }

    public void Clamp()
    {
        lucidity_stock = lucidity_stock < 0 ? 0 : lucidity_stock > 100 ? 100 : lucidity_stock;
    }

    public void Fire()
    {
        if (!Globals.isMulti)
        {
            if (Globals.weapon_id == 1 && Input.GetKeyDown(KeyCode.Mouse1) && stock && onAutre)
            {
                List<Autre> list = new List<Autre>();
                foreach (Autre collider in FindObjectsOfType<Autre>())
                {
                    list.Add(collider);
                }
                if (list.Count < 6)
                {
                    lucidity_stock -= 9.5f;
                    GameObject Clipp = Instantiate(Clip);
                    Clipp.transform.position = Camera.transform.position;
                    Clipp.transform.localEulerAngles = Camera.transform.localEulerAngles + transform.localEulerAngles;
                    Clipp.AddComponent(typeof(Autre));
                    GameObject.Find("Bruitage").SendMessage("play", "tir");
                }
            }
            if (Globals.weapon_id == 2 && Input.GetKeyDown(KeyCode.Mouse0) && stock)
            {
                lucidity_stock -= coefPort;
                Vector3 pos = Camera.transform.position;
                Vector3 rot = Camera.transform.localEulerAngles + transform.localEulerAngles;
                instanciateMe(pos, rot, "l");
            }
            if (Globals.weapon_id == 2 && Input.GetKeyDown(KeyCode.Mouse1) && stock)
            {
                lucidity_stock -= coefPort;
                Vector3 pos = Camera.transform.position;
                Vector3 rot = Camera.transform.localEulerAngles + transform.localEulerAngles;
                instanciateMe(pos, rot, "r");
            }
        }
        else
        {
            if (photonView.isMine && !Globals.Paused)
            {
                bool b = false;
                if (Globals.weapon_id == 1 && Input.GetKeyDown(KeyCode.Mouse1) && stock && onAutre)
                {
                    List<Autre> list = new List<Autre>();
                    foreach (Autre collider in FindObjectsOfType<Autre>())
                    {
                        list.Add(collider);
                    }
                    if (list.Count < 6)
                    {
                        b = true;
                        lucidity_stock -= coefTir;
                        Vector3 pos = Camera.transform.position;
                        Vector3 rot = Camera.transform.localEulerAngles + transform.localEulerAngles;
                        GameObject Clipp = PhotonNetwork.Instantiate("Clips", pos, Quaternion.Euler(rot), 0);
                        photonView.RPC("addName", PhotonTargets.All, Clipp.GetComponent<PhotonView>().viewID, photonView.viewID);
                        photonView.RPC("addType", PhotonTargets.All, Clipp.GetComponent<PhotonView>().viewID, "Autre");
                    }
                }
                if (Globals.weapon_id == 2 && Input.GetKeyDown(KeyCode.Mouse0) && stock)
                {
                    b = true;
                    lucidity_stock -= coefPort;
                    Vector3 pos = Camera.transform.position;
                    Vector3 rot = Camera.transform.localEulerAngles + transform.localEulerAngles;
                    instanciateMe(pos, rot, "l");
                }
                if (Globals.weapon_id == 2 && Input.GetKeyDown(KeyCode.Mouse1) && stock)
                {
                    b = true;
                    lucidity_stock -= coefPort;
                    Vector3 pos = Camera.transform.position;
                    Vector3 rot = Camera.transform.localEulerAngles + transform.localEulerAngles;
                    instanciateMe(pos, rot, "r");
                }
                dep.tirer = b;
            }
        }
    }

    public void instanciateMe(Vector3 pos, Vector3 rot, string LR, string name = "Lance", bool b = false, Vector3 begin = new Vector3())
    {
        if (Globals.isMulti)
        {
            GameObject go = PhotonNetwork.Instantiate(name, pos, Quaternion.Euler(rot), 0);
            photonView.RPC("addType", PhotonTargets.All, go.GetComponent<PhotonView>().viewID, name);
            photonView.RPC("setLR", PhotonTargets.All, go.GetComponent<PhotonView>().viewID, LR, photonView.viewID, b, begin);
            if (b)
            {
                GameObject cam = PhotonNetwork.Instantiate("Camera Objet", pos, Quaternion.Euler(rot + new Vector3(0, 0, 0)), 0);
                photonView.RPC("setCam", PhotonTargets.All, cam.GetComponent<PhotonView>().viewID, go.GetComponent<PhotonView>().viewID, LR, photonView.viewID);
            }
        }
        else
        {
            GameObject go = Instantiate(name == "Lance" ? Lance : Portail);
            go.transform.position = pos;
            go.transform.localEulerAngles = rot;
            Portails po = go.AddComponent<Portails>();
            foreach (Portails goo in FindObjectsOfType<Portails>())
            {
                if (goo.theTruth == LR)
                {
                    Destroy(goo.gameObject);
                }
                else if (goo.theTruth == null)
                {
                    goo.theTruth = LR;
                    goo.stop = b;
                    goo.is_done = b;
                    if (b)
                    {
                        goo.begin = begin;
                    }
                }
            }
            if (b)
            {
                GameObject cam = Instantiate(Resources.Load("Camera Objet") as GameObject);
                SelfCam sc = cam.AddComponent<SelfCam>();
                sc.setSide(LR);
                cam.transform.parent = go.transform;
                cam.transform.localPosition = new Vector3();
                cam.transform.localEulerAngles = new Vector3();
                if (LR == "l")
                {
                    cam.GetComponent<UnityEngine.Camera>().targetTexture = Resources.Load("Left Render") as RenderTexture;
                }
                else
                {
                    cam.GetComponent<UnityEngine.Camera>().targetTexture = Resources.Load("Right Render") as RenderTexture;
                }
            }
        }
    }

    public void Stock()
    {
        stock = lucidity_stock > 0;
    }

    public void isDead()
    {
        if (!stock)
        {
            transform.parent = null;
            GameObject.FindGameObjectWithTag("CheckptsManager").GetComponent<CheckpointsManager>().Respawn();
        }
    }


    // Use this for initialization
    void Start()
    {
        lucidity_check = 100;
        coefTir = 0.95f;
        coefPort = 10f;
        photonView = GetComponent<PhotonView>();
        transmission = GetComponent<Transmission>();
        chat = GameObject.Find("Chat");
        Lance = Resources.Load("Lance") as GameObject;
        Portail = Resources.Load("Portail") as GameObject;
        dep = gameObject.GetComponent<Deplacement>();
    }

    // Update is called once per frame
    void Update()
    {
        temps = Time.deltaTime;
        Stock();
        Get_Lucidity();
        Fire();
        Clamp();
        isDead();
    }

}
