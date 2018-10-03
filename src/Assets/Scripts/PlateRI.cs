using UnityEngine;
using System.Collections;

public class PlateRI : MonoBehaviour
{


    public Vector3 depart;
    public PhotonView photonView;


    public void ResetIt()
    {
        if (!Globals.isMulti)
        {
            transform.position = depart;
        }
    }

    public void mooveOrNot()
    {
        if (Globals.isMulti)
        {
            if (PhotonNetwork.isMasterClient)
            {
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


    // Use this for initialization
    void Start()
    {
        depart = transform.position;
        photonView = GetComponent<PhotonView>();
        photonView.ObservedComponents.Add(GetComponent<PlateRI>());
    }

    // Update is called once per frame
    void Update()
    {
        mooveOrNot();
    }
}
