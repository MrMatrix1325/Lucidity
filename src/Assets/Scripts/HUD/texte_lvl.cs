using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class texte_lvl : MonoBehaviour {

    public Text txt;
	// Use this for initialization
	void Start () {
        Scene n = SceneManager.GetActiveScene();
        string name = n.name;
        if (!Globals.isMulti)
        {
            txt.text = name + " : solo";
        }
        else
        {
            txt.text = name + " : multi";
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
