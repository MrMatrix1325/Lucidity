using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkManager : MonoBehaviour {


    public bool isAtStartup = true;
    NetworkClient myClient;

    // Use this for initialization
    void Start()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isAtStartup)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetupServer();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                SetupClient();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                SetupServer();
                SetupLocalClient();
            }
        }
    }
    void OnGUI()
    {
        if (isAtStartup)
        {
            GUI.Label(new Rect(2, 10, 150, 100), "Press S for server");
            GUI.Label(new Rect(2, 30, 150, 100), "Press B for both");
            GUI.Label(new Rect(2, 50, 150, 100), "Press C for client");
        }
        GUI.Label(new Rect(2, 70, 150, 100), "Connected : " + BooltoString(myClient.isConnected));
    }

    public string BooltoString(bool b)
    {
        string res = "";
        if (b)
        {
            res = "True";
        }
        else
        {
            res = "False";
        }
        return res;
    }

    // Create a server and listen on a port
    public void SetupServer()
    {
        NetworkServer.Listen(4444);
        isAtStartup = false;
    }

    // Create a client and connect to the server port
    public void SetupClient()
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect("127.0.0.1", 4444);
        isAtStartup = false;
    }

    // Create a local client and connect to the local server
    public void SetupLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        isAtStartup = false;
    }

    // CLient function
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
