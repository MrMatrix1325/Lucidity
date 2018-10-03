using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour {


    public string[] audioName;
    public AudioClip[] audioClip;
    public AudioSource music;
    bool clipFound;
    public OptionsMenuScript Volume;
    public bool paused;

    // Use this for initialization
    void Start ()
    {
        //music.volume = Volume.getvolume();
        music.volume = PlayerPrefs.HasKey("Volume") ? PlayerPrefs.GetFloat("Volume") : 42;
    }
	
	// Update is called once per frame
	void Update ()
    {

        //{
        //    if (!paused && Input.GetButtonDown("Cancel"))
        //    {
        //        paused = true;
        //        music.Pause();
        //    }
        //    else if (Input.GetButtonDown("Cancel"))
        //    {
        //        paused = false;
        //        music.UnPause();
        //    }
        //}
    }

    public void play(string clipName)
    {
        for (int i = 0; i < audioName.Length; i++)
        {
            if (clipName == audioName[i])
            {

                music.clip = audioClip[i];
                if (clipName != "footstep")
                {
                    music.loop = !enabled;
                }
                music.Play();
                
                clipFound = true;
                break;       
            }
            else
            {
                clipFound = false;
            }

        }
        if (!clipFound)
        {
            Debug.Log("Clip not found");

        }


    }
    public void stopLoop(string stop)
    {
        if (stop == "stop")
        {
            music.loop = !enabled;
        }
        else
        {
            music.loop = enabled;
        }
    }
}
