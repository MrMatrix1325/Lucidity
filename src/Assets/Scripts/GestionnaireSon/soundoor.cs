using UnityEngine;
using System.Collections;

public class soundoor : MonoBehaviour
{
    public string[] audioName;
    public AudioClip[] audioClip;
    public AudioSource music;
    bool clipFound;
    public bool paused;
    public OptionsMenuScript Volume;

    // Use this for initialization
    void Start()
    {
        //music.volume = Volume.getvolume();
        music.volume = PlayerPrefs.HasKey("Volume") ? PlayerPrefs.GetFloat("Volume") : 42;
    }

    // Update is called once per frame
    void Update()
    {
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        //    {
        //        paused = true;
        //        music.Pause();
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Escape))
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
            //Debug.Log("loop stop");
            music.loop = !enabled;
        }
        else
        {
            music.loop = enabled;
        }
    }
}
