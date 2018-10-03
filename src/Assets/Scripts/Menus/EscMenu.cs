using UnityEngine;
using System.Collections;

public class EscMenu : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject OptionsCanvas;
    public AudioManager audioMan;
    public SoundManager soundMan;
    public soundoor bruitage;
    private Camera _camera;
    private Deplacement _deplacement;

    void Start()
    {
        if (Globals.isMulti)
            enabled = GetComponent<PhotonView>().isMine;
        GameObject Menus = GameObject.Find("Menus");
        MainCanvas = Menus.transform.Find("PauseMenu").gameObject;
        OptionsCanvas = Menus.transform.Find("OptionsMenu").gameObject;
        audioMan = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        soundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        bruitage = GameObject.Find("Bruitage").GetComponent<soundoor>();
        MainCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!enabled)
            return;
        if (Input.GetButtonDown("Cancel"))
        {
            if (Globals.Paused)
            {
                audioMan.music.UnPause();
                soundMan.music.UnPause();
                bruitage.music.UnPause();
                audioMan.paused = false;
                soundMan.paused = false;
                bruitage.paused = false;
                MainCanvas.SetActive(false);
                OptionsCanvas.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Globals.Paused = false;
            }
            else
            {
                audioMan.music.Pause();
                soundMan.music.Pause();
                bruitage.music.Pause();
                audioMan.paused = true;
                soundMan.paused = true;
                bruitage.paused = true;
                MainCanvas.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Globals.Paused = true;
            }
        }
    }
    public void Resume()
    {
        if (Globals.Paused)
        {
            audioMan.music.UnPause();
            soundMan.music.UnPause();
            bruitage.music.UnPause();
            audioMan.paused = false;
            soundMan.paused = false;
            bruitage.paused = false;
            MainCanvas.SetActive(false);
            OptionsCanvas.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Globals.Paused = false;
        }
    }
}
