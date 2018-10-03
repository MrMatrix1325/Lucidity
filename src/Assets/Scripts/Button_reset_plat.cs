using UnityEngine;
using System.Collections;

public class Button_reset_plat : MonoBehaviour
{

    public GameObject target;
    public Vector3 init_pos;
    public PhotonView photonview;

    // Use this for initialization
    void Start()
    {
        init_pos = target.transform.position;
        photonview = gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (Globals.isMulti)
        {

            if (other.tag == "Joueur" && Input.GetButtonDown("Interaction"))
            {
                photonview.RPC("Reset_plat", PhotonTargets.All, init_pos);
            }
        }
        else
            target.transform.position = init_pos;
    }

    [PunRPC]
    public void Reset_plat(Vector3 init_poss)
    {
        target.transform.position = init_pos;
    }

}
