using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour {

    public EscMenu menu;
    public GameObject MainCanvas;
    public GameObject OptionsCanvas;

	// Use this for initialization
	void Start () {
        //menu = GameObject.Find("Corps").GetComponent<EscMenu>();
        menu = GameObject.FindGameObjectWithTag("Joueur").GetComponent<EscMenu>();
        MainCanvas = GameObject.Find("PauseMenu");
        //OptionsCanvas = GameObject.Find("OptionsMenu");

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        /*GameObject Player = GameObject.Find("CheckptsManager").GetComponent<CheckpointsManager>().Player;
        Player.GetComponent<Deplacement>().enabled = false;
        Player.GetComponentInChildren<Camera>().enabled = false;*/
        MainCanvas.SetActive(false);
        if(Globals.isMulti)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.Disconnect();
            Globals.MainPlayer.GetComponent<ChatHandler>().LeaveAllChannels();
        }
        SceneManager.LoadScene(0);
    }

    public void Options()
    {
        OptionsCanvas.SetActive(true);
        MainCanvas.SetActive(false);
        
    }

    public void Resume()
    {
        menu.Resume();
    }
}
