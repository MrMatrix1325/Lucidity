using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LucidityBar : MonoBehaviour
{

    public Stuff Player;
    public Image bar;
    public Text text;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            bar.transform.localScale = new Vector3(Player.lucidity_stock / 100, 1, 1);
            text.text = "Lucidité: " + ((int)Player.lucidity_stock).ToString() + "%";
        }
    }
}

