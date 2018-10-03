using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public OptionsMenuScript Volume;
    public bool paused;
    //The highest a sound effect will be randomly pitched.

    void Start()
    {
        //music.volume = Volume.getvolume();
        music.volume = PlayerPrefs.HasKey("Volume") ? PlayerPrefs.GetFloat("Volume") : 42;
        paused = false;
    }
    void Update()
    {
        //if (Input.GetButtonDown("Cancel") && !paused)
        //{
        //    paused = true;
        //    music.Pause();
        //}
        //else if (Input.GetButtonDown("Cancel"))
        //{
        //    paused = false;
        //    music.UnPause();
        //}
    }

}

