using UnityEngine;
using System.Collections;

public class Transmission : MonoBehaviour
{

    private PhotonView photonView;
    private GameObject camera;


    public void sort(string str, string name, string side = "")
    {
        switch (str)
        {
            case "addType":
                Vector3 pos = camera.transform.position;
                Vector3 rot = camera.transform.localEulerAngles + gameObject.transform.localEulerAngles;
                GameObject go = PhotonNetwork.Instantiate(name, pos, Quaternion.Euler(rot), 0);
                /*photonView.RPC("addType", PhotonTargets.All, go.GetComponent<PhotonView>().viewID, name);
                photonView.RPC("addName", PhotonTargets.All, go.GetComponent<PhotonView>().viewID, photonView.viewID);*/
                break;
            case "setLR":
                break;
            case "setCam":
                break;
        }
    }

    [PunRPC]
    public void addType(int ID, string component)
    {
        GameObject goo = gameObject;
        string nul = component == "Autre" ? "Clips" : component;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag(nul))
        {
            if (go.GetComponent<PhotonView>().viewID == ID)
            {
                goo = go.gameObject;
            }
        }
        if (component == "Autre")
        {
            goo.AddComponent<Autre>();
        }
        else if (component == "Lance" || component == "Portail")
        {
            goo.AddComponent<Portails>();
        }
    }

    [PunRPC]
    public void setLR(int ID, string LR, int joueur_ID, bool b = false, Vector3 begin = new Vector3())
    {
        foreach (Portails go in FindObjectsOfType<Portails>())
        {
            if (go.gameObject.GetComponent<PhotonView>().viewID == ID)
            {

                go.theTruth = LR;
                go.joueur_ID = joueur_ID;
                go.stop = b;
                go.is_done = b;
                if (b)
                {
                    go.begin = begin;
                }
            }
            else
            {
                if (go.theTruth == LR && go.joueur_ID == joueur_ID)
                {
                    go.destroy();
                }
            }
        }
    }

    [PunRPC]
    public void setCam(int self_ID, int parent_ID, string LR, int joueur_ID)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Camera Objet"))
        {
            if (go.GetComponent<PhotonView>().viewID == self_ID)
            {
                foreach (GameObject goo in GameObject.FindGameObjectsWithTag("Portail"))
                {
                    if (goo.GetComponent<PhotonView>().viewID == parent_ID)
                    {
                        go.transform.parent = goo.transform;
                    }
                }
                SelfCam sc = go.AddComponent<SelfCam>();
                sc.setSide(LR);
                sc.setBuilder(joueur_ID);
                if (LR == "l")
                {
                    sc.GetComponent<UnityEngine.Camera>().targetTexture = Resources.Load("Left Render") as RenderTexture;
                }
                else
                {
                    sc.GetComponent<UnityEngine.Camera>().targetTexture = Resources.Load("Right Render") as RenderTexture;
                }
            }
        }
    }

    [PunRPC]
    public void addName(int ID, int name)
    {
        GameObject goo = gameObject;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Clips"))
        {
            if (go.GetComponent<PhotonView>().viewID == ID)
            {
                goo = go.gameObject;
            }
        }
        goo.name = name.ToString();
    }

    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Stuff>().Camera;
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
