using UnityEngine;
using System.Collections;

public class Button_obj : MonoBehaviour {

    public Bouton bouton1;
    public Bouton bouton2;
    public Bouton bouton3;
    public GameObject objectifs;
    public GameObject TextInter;
    bool msg = true;
    bool ms1 = true;
    bool msg2 = true;
	// Use this for initialization
	void Start ()
    {
        msg = true;
        objectifs = GameObject.Find("objectifs");
        TextInter = GameObject.Find("TextInter");
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (!bouton1.push && !bouton2.push && !bouton3.push)
        {
            TextInter.SendMessage("addText", "Bienvenue dans le niveau 2, tu vas devoir trouver 3 boutons, pour les activer appuie sur F à proximité d'un bouton");
            objectifs.SendMessage("print", "Trouver les boutons (reste 3 sur 3)");
        }
        else if (bouton1.push && !bouton2.push && !bouton3.push  || !bouton1.push && !bouton2.push && bouton3.push || !bouton1.push && bouton2.push && !bouton3.push)
        {
            if (msg)
            {
                objectifs.SendMessage("replace" , "Trouver les boutons (reste 2 sur 3)");
                msg = false;
            }

        }
        else if (bouton1.push && bouton2.push && !bouton3.push || !bouton1.push && bouton2.push && !bouton3.push || !bouton1.push && !bouton2.push && bouton3.push)
        {
            if (ms1)
            {
                objectifs.SendMessage("replace", "Trouver les boutons (reste 1 sur 3)");
                ms1 = false;
            }

        }
        else if (bouton1.push && bouton2.push && bouton3.push)
        {
            if (msg2)
            {
                objectifs.SendMessage("replace", "Trouver les boutons ");
                objectifs.SendMessage("isCompleted");
                objectifs.SendMessage("print", "Utiliser la plateforme pour se diriger vers la sortie");
                TextInter.SendMessage("addText", "Utilise la plateforme pour arriver jusqu'à la sortie");
                msg2 = false;
            }

        }
        else
        {
            TextInter.SendMessage("addText", "Bienvenue dans le niveau 2, tu vas devoir trouver 3 boutons, pour les activer appuie sur F à proximité d'un bouton");
            objectifs.SendMessage("print", "Trouver les boutons (reste 3 sur 3)");
        }

    }
}
