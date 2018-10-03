using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour {

    public GameObject menu;

	// Use this for initialization
	void Start () {
        menu = GameObject.Find("EndMenu");
        menu.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EndGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
