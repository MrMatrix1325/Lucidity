using UnityEngine;
using System.Collections;

public class click_mm : MonoBehaviour {

    public string clip;
    public AudioClip clipa;
    public AudioSource music;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void play(string clp)
    {
        if (clip == clp )
        {
            music.clip = clipa;
            music.Play();

        }
    }
}
