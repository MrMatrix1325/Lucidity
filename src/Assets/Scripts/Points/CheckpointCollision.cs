using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CheckpointCollision : MonoBehaviour
{
    public Text TextInter;
    public CheckpointsManager chkmngr;
    public GameObject endMenu;
    public GameObject Player;
    public GameObject Bruitage;
    public bool check = false;
    GameObject[] sphereGroups;

    // Use this for initialization
    void Start()
    {
        chkmngr = GameObject.FindGameObjectWithTag("CheckptsManager").GetComponent<CheckpointsManager>();
        if (Globals.isMulti)
        {
            if (GetComponent<PhotonView>().isMine)
            {
                chkmngr.Player = gameObject;
                GameObject.FindObjectOfType<LucidityBar>().Player = gameObject.GetComponent<Stuff>();
            }

        }
        else
        {
            chkmngr.Player = GameObject.Find("Corps");
            sphereGroups = GameObject.FindGameObjectsWithTag("Sphere");
        }

        enabled = !(Globals.isMulti && !GetComponentInParent<PhotonView>().isMine);

        if (TextInter != null)
            TextInter = GameObject.Find("TextInter").GetComponent<Text>();
        //endMenu = GameObject.Find("EndMenu");
        Player = gameObject;
        Bruitage = GameObject.Find("Bruitage");
        endMenu = GameObject.Find("Menus").transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (!enabled)
            return;
        if (other.gameObject.tag == "Checkpoint" && chkmngr.CurrentCheckpoint != other.gameObject)
        {
            Player.GetComponent<Stuff>().lucidity_check = Player.GetComponent<Stuff>().lucidity_stock;
            chkmngr.CurrentCheckpoint = other.gameObject;
            Debug.Log("Checkpoint Set !");
            check = true;
            Bruitage.SendMessage("play", "Exclamation");
        }

        if (other.gameObject.tag == "EndPoint")
        {
            endMenu.SetActive(true);
            Globals.Paused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (Globals.isMulti)
                GameObject.Find("NetworkHolder").GetComponent<NetworkController>().SwitchLevel();
            else
            {
                if (PlayerPrefs.HasKey("CompletedLvl") && PlayerPrefs.GetInt("CompletedLvl") < SceneManager.GetActiveScene().buildIndex)
                    PlayerPrefs.SetInt("CompletedLvl", SceneManager.GetActiveScene().buildIndex);
            }
        }
        if(other.gameObject.tag == "NextPoint")
        {
            GameObject.Find("objectifs").SendMessage("isCompleted");
            Debug.Log("Next Lvl");
            if (PlayerPrefs.GetInt("CompletedLvl") < SceneManager.GetActiveScene().buildIndex)
                PlayerPrefs.SetInt("CompletedLvl", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!enabled)
            return;
        if (col.gameObject.tag == "DieZone")
        {
            GameObject.FindGameObjectWithTag("CheckptsManager").GetComponent<CheckpointsManager>().Respawn();
            foreach (GameObject e in sphereGroups)
                e.GetComponent<PointApparition>().Reset();
            Bruitage.SendMessage("play", "Die");
        }
    }
}
