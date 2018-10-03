using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class objectif_lvl3 : MonoBehaviour {

    public Bouton bouton1;
    public Bouton bouton2;
    public PointApparition pointappa;
    public Bouton bouton3;
    public GameObject objectifs;
    bool mgs = true;
    bool mgs2 = true;
    bool mgs3 = true;
    bool mgs4 = true;
    public GameObject textinter;
	// Use this for initialization
	void Start () {
        objectifs = GameObject.Find("objectifs");
        textinter = GameObject.Find("TextInter");
	}
	
	// Update is called once per frame
	void Update () {

        if (bouton1.push)
        {
            if (mgs)
            {
                textinter.SendMessage("addText" , "Bien joué! Retournes sur tes pas pour trouver le deuxième bouton");
                objectifs.SendMessage("isCompleted");
                objectifs.SendMessage("print", "Trouver le deuxième bouton.");
                mgs = false;
            }

            if(bouton2.push)
            {
                if (mgs2)
                {
                    textinter.SendMessage("addText", "Bravo, tu dois maintenant trouver la sortie du labyrinthe de plateforme");
                    objectifs.SendMessage("isCompleted");
                    objectifs.SendMessage("print", "Trouver la sortie du labyrinthe de plateforme.");
                    mgs2 = false;
                }

                if (pointappa.done)
                {
                    if (mgs3)
                    {
                        textinter.SendMessage("addText", "Réutillises les portails pour trouver le troisème bouton.");
                        objectifs.SendMessage("isCompleted");
                        objectifs.SendMessage("print", "Trouver le troisième bouton à l'aide des portails.");
                        mgs3 = false;
                    }


                    if (bouton3.push)
                    {
                        if (mgs4)
                        {
                            textinter.SendMessage("addText", "Utilises la plateforme guidable pour trouver la sortie.");
                            objectifs.SendMessage("isCompleted");
                            objectifs.SendMessage("print", "Trouver la sortie");
                            mgs4 = false;
                        }

                    }
                }
            }
        }
        else
        {
            textinter.SendMessage("addText", "Bienvenue dans le niveau 3. Pour franchir cette étape tu vas devoir te servir des portails. Fait un clique gauche pour lancer le premier portail et appuies sur E pour lancer le deuxième");
            objectifs.SendMessage("print", "Trouver le premier bouton");
        }
	
	}
}
