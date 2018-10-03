using UnityEngine;
using System.Collections;

public class PorteMsg : MonoBehaviour
{

    public Porte door;
    public Porte door2;
    public Porte door3;
    public Porte door13;
    public GameObject objectifs;
    public GameObject TextInter;
    public bool msg;
    public bool ok = true;

    public CheckpointCollision chm;

    // Use this for initialization
    void Start()
    {
        msg = false;
        ok = true;
        objectifs = GameObject.Find("objectifs");
        TextInter = GameObject.Find("TextInter");        
    }

    // Update is called once per frame
    void Update()
    {
        if (!door.open && !chm.check)
        {
            TextInter.SendMessage("addText", "Bienvenue dans notre jeu, nous allons vous apprendre les commandes de bases. Pour ouvrir une porte appuyer sur F à proximité de la porte");
            objectifs.SendMessage("print", "Ouvrir la première porte et se déplacer avec les touches 'Z' 'Q' 'S' et 'D'");
        }
        if (!door2.open && door.open && !chm.check)
        {
            if (ok)
            {
                objectifs.SendMessage("isCompleted");
                ok = false;
            }
            objectifs.SendMessage("print", "Trouver la sortie");
            TextInter.SendMessage("addText", "Tu vas devoir traverser un labyrinthe , ton objectif , trouver la sortie !");
        }
        else if (!door3.open && door2.open && door.open && !door13.open && !chm.check)
        {
            TextInter.SendMessage("addText", "Pour cela tu vas avoir besoin d'une ressource essentiel tout au long de ton périple");
        }
        else if (door3.open && door2.open && !door13.open && !chm.check)
        {
            TextInter.SendMessage("addText", "Voici une source de lucidité , elle te permet de recharger ta lucidité, la ressource primaire du jeu. Bonne chance");
        }
        else if ((door13.open || chm.check))
        {
            TextInter.SendMessage("addText", "Tu vas maintenant utiliser une plateforme grise pour cela fait clic droit sur un mur et clic droit sur la plateforme , la touche espace te permet de sauter");
            objectifs.SendMessage("print", "Aller à la fin du jeu");
        }


    }
}
