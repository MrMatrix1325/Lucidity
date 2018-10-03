using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{


    float mouse_rot_y;
    float max_y = 80f;
    float min_y = -80f;
    float temps;
    PhotonView view;



    public void Rotate_Set()
    {
        mouse_rot_y += Input.GetAxis("Mouse Y") * temps * (PlayerPrefs.GetFloat("Sensitivity") * 2);
        mouse_rot_y = Mathf.Clamp(mouse_rot_y, min_y, max_y);
    }

    // Use this for initialization
    void Start()
    {
        view = transform.parent.transform.parent.GetComponent<PhotonView>();
        if (Globals.isMulti && !view.isMine)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Globals.Paused)
        {
            temps = Time.deltaTime;
            //if (!Globals.isMulti || view.isMine)
            Rotate_Set();
            transform.localEulerAngles = new Vector3(-mouse_rot_y, 0, 0);
        }
    }
}
