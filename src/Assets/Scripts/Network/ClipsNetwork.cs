using UnityEngine;
using System.Collections;

public class ClipsNetwork : Photon.MonoBehaviour{

    //public PhotonView photonView;
    public Vector3 correctPlayerPos;
    public Quaternion correctPlayerRot;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("Blabla");
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, receive data
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }


    // Use this for initialization
    void Start ()
    {
        //photonView = gameObject.GetComponent<PhotonView>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        correctPlayerPos += new Vector3(0, 0, 10F);
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, 1);
        }
    }
}
