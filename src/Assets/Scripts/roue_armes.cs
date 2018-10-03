using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class roue_armes : MonoBehaviour
{
    public GameObject w1;
    public GameObject w2;
    public List<GameObject> weapons;
    public Text Statustext;
    public bool isMine;
    public PhotonView photonView;


    // Use this for initialization
    void Start()
    {
        Globals.weapon_id = 0;
        photonView = gameObject.GetComponent<PhotonView>();
        isMine = !Globals.isMulti || gameObject == Globals.MainPlayer;
        weapons.Add(new GameObject());
        weapons.Add(w1);
        weapons.Add(w2);
        Statustext = GameObject.Find("Texte outil").transform.GetChild(0).GetComponent<Text>();
        Statustext.text = "Outil: Rien";
        switch_wp(Globals.weapon_id);
    }

    // Update is called once per frame
    void Update()
    {
        while(Statustext == null)
        {
            Statustext = GameObject.Find("Texte outil").transform.GetChild(0).GetComponent<Text>();
            Statustext.text = "Outil: Rien";
        }
        changeweapon();
    }

    void changeweapon()
    {
        if (isMine && !Globals.Paused && Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Globals.weapon_id = (Globals.weapon_id + 1) % weapons.Count;
            if (Globals.isMulti)
                photonView.RPC("switch_wp", PhotonTargets.All, Globals.weapon_id);
            else
                switch_wp(Globals.weapon_id);
            switch (Globals.weapon_id)
            {
                case 0:
                    Statustext.text = "Outil: Rien";
                    break;
                case 1:
                    Statustext.text = "Outil: Clips";
                    break;
                case 2:
                    Statustext.text = "Outil: Portails";
                    break;
            }
        }
        if (isMine && !Globals.Paused && Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Globals.weapon_id = Globals.weapon_id == 0 ? weapons.Count - 1 : Globals.weapon_id - 1;
            if (Globals.isMulti)
                photonView.RPC("switch_wp", PhotonTargets.All, Globals.weapon_id);
            else
                switch_wp(Globals.weapon_id);
            switch (Globals.weapon_id)
            {
                case 0:
                    Statustext.text = "Outil: Rien";
                    break;
                case 1:
                    Statustext.text = "Outil: Clips";
                    break;
                case 2:
                    Statustext.text = "Outil: Portails";
                    break;
            }
        }
    }

    [PunRPC]
    public void switch_wp(int id)
    {
        foreach (GameObject wp in weapons)
            wp.SetActive(false);
        weapons[id].SetActive(true);
    }
}
