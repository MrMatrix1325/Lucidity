using UnityEngine;
using System.Collections;

public class Deplacement : Photon.MonoBehaviour
{
    //Attributs de la classe :

    float temps;
    //float mouse_x = Input.mousePosition.x;
    //float mouse_y = Input.mousePosition.y;
    //float speed = 10f;
    float mouse_rot_x = 0f;
    //float mouse_rot_y = 0f;
    //float sens_x = 10f;
    //float sens_y = 10f;
    //float masse = 60f;
    //float newton = 10f;
    public float jump_time = 1f;
    //float ini_jump = 0f;
    //float max_y = 80f;
    //float min_y = -80f;
    public Vector3 Trans_v = Vector3.zero;
    public Vector3 Rot_v = Vector3.zero;
    public float sensitivity;
    public bool DoIt;
    public float avant;
    public float apres = 0f;
    public bool déplacement;
    public GameObject audioManager;
    PhotonView view;
    private Vector3 correctPos;
    private Quaternion correctRot;
    public Animator anim;
    public bool Run = false;
    public float RunV = 0;
    public float RunH = 0;
    public bool Jump = false;
    public float Idle = 0;
    public bool tirer = false;
    //System.Random ran = new System.Random();
    //public enum CharacterState {Idle, RunForward, RunBackward, RunLeft, RunRight };


    //Méthodes :
    void Translate_set()
    {

        /*Idle = ran.Next(1, 6);
        anim.SetFloat("Idle", Idle);*/
        déplacement = false;
        if (!Globals.isMulti || photonView.isMine)
        {
            RunV = Input.GetAxis("Vertical");
            RunH = Input.GetAxis("Horizontal");
        }
        if (RunV != 0 || RunH != 0)
        {
            Run = true;
        }
        else
        {
            Run = false;
        }
        anim.SetFloat("RunV", RunV);
        anim.SetFloat("RunH", RunH);
        anim.SetBool("Run", Run);
        anim.SetBool("Fire", tirer);
        if (RunV < 0)
        {
            anim.SetFloat("Speed", -1);
        }
        else
        {
            anim.SetFloat("Speed", 1);
        }
        Trans_v.x = RunH * temps * 6 / 1f;
        Trans_v.z = RunV * temps * 6 / 1f;

        /*if (Input.GetButton("Up"))
        {
            if (Input.GetButton("Down"))
            {
                //Trans_v.z = 0 * temps;
            }
            else if (Input.GetButton("Left") && Input.GetButton("Right"))
            {
                //Trans_v.z = 5 * temps;
            }
            else if (Input.GetButton("Left") || Input.GetButton("Right"))
            {
                //Trans_v.z = Mathf.Sqrt(5) * temps;
            }
            else
            {
                //Trans_v.z = 5 * temps;
            }
            déplacement = true;
            if (Input.GetButtonDown("Up"))
            {
                audioManager.SendMessage("play", "footstep");
                audioManager.SendMessage("stopLoop", "non");
            }
        }
        else
        {

        }
        if (Input.GetButton("Down"))
        {
            if (Input.GetButton("Up"))
            {
                //Trans_v.z = 0 * temps;
            }
            else if (Input.GetButton("Left") && Input.GetButton("Right"))
            {
                //Trans_v.z = - 5 * temps;
            }
            else if (Input.GetButton("Left") || Input.GetButton("Right"))
            {
                //Trans_v.z = - Mathf.Sqrt(5) * temps;
            }
            else if (Input.GetButton("Up"))
            {
                //Trans_v.z = 0 * temps;
            }
            else
            {
                //Trans_v.z = - 5 * temps;
            }
            déplacement = true;
            if (Input.GetButtonDown("Down"))
            {
                audioManager.SendMessage("play", "footstep");
                audioManager.SendMessage("stopLoop", "non");
            }
        }
        if (Input.GetButton("Left"))
        {
            if (Input.GetButton("Right"))
            {
                //Trans_v.x = 0 * temps;
            }
            else if (Input.GetButton("Up") && Input.GetButton("Down"))
            {
                //Trans_v.x = - Mathf.Sqrt(5) * temps;
            }
            else if (Input.GetButton("Up") || Input.GetButton("Down"))
            {
                //Trans_v.x = - Mathf.Sqrt(5) * temps;
            }
            else
            {
                //Trans_v.x = -5 * temps;
            }
            déplacement = true;
            if (Input.GetButtonDown("Left"))
            {
                audioManager.SendMessage("play", "footstep");
                audioManager.SendMessage("stopLoop", "non");
            }
        }
        if (Input.GetButton("Right"))
        {
            if (Input.GetButton("Left"))
            {
                //Trans_v.x = 0 * temps;
            }
            else if (Input.GetButton("Up") && Input.GetButton("Down"))
            {
                //Trans_v.x = Mathf.Sqrt(5) * temps;
            }
            else if (Input.GetButton("Up") || Input.GetButton("Down"))
            {
                //Trans_v.x = Mathf.Sqrt(5) * temps;
            }
            else if (Input.GetButton("Up") || Input.GetButton("Down"))
            {
                //Trans_v.x = Mathf.Sqrt(5) * temps;
            }
            else
            {
                //Trans_v.x = 5 * temps;
            }
            déplacement = true;
            if (Input.GetButtonDown("Right"))
            {
                audioManager.SendMessage("play", "footstep");
                audioManager.SendMessage("stopLoop", "non");
            }
        }*/
        sound();
    }
    public void sound()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            audioManager.SendMessage("play", "footstep");
            audioManager.SendMessage("stopLoop", "non");
        }
        else
        {
            audioManager.SendMessage("stopLoop", "stop");
        }

    }


    void Translate_reset()
    {
        float y = Trans_v.y;
        Trans_v = new Vector3(0, y, 0);
    }

    void Jump_set()
    {
        if (photonView.isMine || !Globals.isMulti)
        {
            if (Input.GetButtonDown("Jump") && jump_time == 1)
            {
                Jump = true;
                avant = transform.position.y;
                DoIt = true;
            }
            else
            {
                Jump = false;
            }
            anim.SetBool("Jump", Jump);
            if (DoIt)
            {
                Trans_v.y = 5f * temps * jump_time;
                jump_time -= temps;
            }
            else
            {
                Trans_v.y = 0;
                jump_time = 1f;
            }
            if (avant - apres > 0)
            {
                DoIt = false;
            }
            avant = apres;
            apres = transform.position.y;
        }
    }

    void Jump_reset()
    {
        if (jump_time <= 0)
        {
            DoIt = false;
        }
    }

    void Rotate_set()
    {
        mouse_rot_x += Input.GetAxis("Mouse X") * temps * PlayerPrefs.GetFloat("Sensitivity") * 2;
        transform.localEulerAngles = new Vector3(0, mouse_rot_x, 0);
    }

    void Rotate_reset()
    {

    }

    public void Rotate_P(Vector3 vect)
    {
        mouse_rot_x += vect.y;
    }


    // Use this for initialization
    void Start()
    {
        //transform.position = new Vector3(1, 1, 1);
        view = GetComponent<PhotonView>();
        audioManager = GameObject.Find("AudioManager");
        correctPos = transform.position;
        correctRot = transform.rotation;
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        //Refresh des variables
        temps = Time.deltaTime;
        //Reset des méthodes
        Translate_reset();
        Jump_reset();

        //Set des méthodes
        if (!Globals.isMulti)
        {
            if (!Globals.Paused)
            {
                Translate_set();
                Jump_set();
                Rotate_set();
                gameObject.transform.Translate(Trans_v);
            }
        }
        else
        {
            if (!Globals.Paused && view.isMine)
            {
                Translate_set();
                Jump_set();
                Rotate_set();
                gameObject.transform.Translate(Trans_v);
            }
            else if (!view.isMine)
            {
                transform.position = Vector3.Lerp(transform.position, correctPos, Time.deltaTime * 5);
                transform.rotation = Quaternion.Lerp(transform.rotation, correctRot, Time.deltaTime * 15);
            }
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(RunV);
            stream.SendNext(RunH);
            stream.SendNext(Jump);
            stream.SendNext(tirer);
        }
        else
        {
            // Network player, receive data
            correctPos = (Vector3)stream.ReceiveNext();
            correctRot = (Quaternion)stream.ReceiveNext();
            RunV = (float)stream.ReceiveNext();
            RunH = (float)stream.ReceiveNext();
            Jump = (bool)stream.ReceiveNext();
            anim.SetBool("Jump", Jump);
            tirer = (bool)stream.ReceiveNext();
            Translate_set();
        }
    }
}
