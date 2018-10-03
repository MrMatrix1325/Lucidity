using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoloSelect : MonoBehaviour {

    //public GameObject MainCanvas;
    public AudioManager clip;
    public Button Lvl2Button;
    public Button Lvl3Button;
    public GameObject MainCanvas;
    public GameObject canvas;

    int FirstSoloLvl = 1;

    // Use this for initialization
    void Start()
    {
        Lvl2Button.interactable = PlayerPrefs.GetInt("CompletedLvl") >= FirstSoloLvl;
        Lvl3Button.interactable = PlayerPrefs.GetInt("CompletedLvl") >= FirstSoloLvl + 1;
        Globals.Paused = false;
        clip = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame

    public void MainMenu()
    {
        //SceneManager.LoadScene(0);
        MainCanvas.SetActive(true);
        canvas.SetActive(false);
    }

    public void Lvl1()
    {
        Globals.isMulti = false;
        SceneManager.LoadScene(FirstSoloLvl);
        clip.play("Musique");

    }
    public void Lvl2()
    {
        Globals.isMulti = false;
        SceneManager.LoadScene(FirstSoloLvl+1);
        clip.play("Musique");
    }

    public void Lvl3()
    {
        Globals.isMulti = false;
        SceneManager.LoadScene(FirstSoloLvl + 2);
        clip.play("Musique");
    }
}
