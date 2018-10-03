using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PositionHUD : MonoBehaviour
{

    public Stuff Player;
    public Image bar;
    public Text text;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Corps").GetComponent<Stuff>();

    }

    // Update is called once per frame
    void Update()
    {
       // bar.transform.position = new Vector3(bar.transform.position.x * Screen.width ,  bar.transform.position.y * Screen.height, bar.transform.position.z);
    }
}

