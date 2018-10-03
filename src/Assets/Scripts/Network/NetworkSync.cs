using UnityEngine;
using System.Collections;

public class NetworkSync : Photon.MonoBehaviour {

    private Vector3 correctPos;
    private Quaternion correctRot;
    private PhotonView view;

	// Use this for initialization
	void Start () {
        view = GetComponent<PhotonView>();
        if (!Globals.isMulti)
            Globals.MainPlayer = gameObject;
        else if(Globals.isMulti && !view.isMine)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
	}
	
    // Update is called once per frame
    void Update()
    {
    }

}
