using UnityEngine;
using System.Collections;

public class Sonclick : MonoBehaviour {

    public AudioSource music;
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
        play();
	}

    public void play()
    {
        music.Play();
    }
}
