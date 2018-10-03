using UnityEngine;
using System.Collections;

public class HideGroup : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Bouton");
        foreach(GameObject go in list)
        {
            for (int i = 1; i < 3; i++)
            {
                if (gameObject.name == "Groupe " + i && go.name == "Bouton " + i)
                {
                    go.GetComponent<Bouton>().group = this;
                }
            }
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void notHide()
    {
        gameObject.SetActive(true);
    }
}
