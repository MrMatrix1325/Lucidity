using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject OptionsCanvas;
    public GameObject LoginCanvas;
    public GameObject SoloSelect;
    public AudioClip clip;

    //public int multiSceneId = 4;

    // Use this for initialization
    void Start()
    {
        OptionsCanvas.SetActive(false);
        Globals.Paused = false;
        //LoginCanvas = GameObject.Find("MutiLogin");
        LoginCanvas.SetActive(false);
        SoloSelect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
        
    }

    public void Solo()
    {
        Globals.isMulti = false;
        Globals.Paused = false;
        //SceneManager.LoadScene(1);
        SoloSelect.SetActive(true);
        MainCanvas.SetActive(false);
    }

    public void Multi()
    {
        Globals.isMulti = true;
        //SceneManager.LoadScene(multiSceneId);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MainCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
        LoginCanvas.SetActive(true);
    }

    public void Options()
    {
        OptionsCanvas.SetActive(true);
        MainCanvas.SetActive(false);
    }

}