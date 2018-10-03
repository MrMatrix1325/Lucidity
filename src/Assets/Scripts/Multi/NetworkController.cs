using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviour
{
    private string gameVersion = "0.2";
    public Button joinButton;
    public Button hostButton;
    public Text statusText;
    public GameObject multiLobbyCanvas;
    public GameObject player;
    public Dropdown dropdown;
    public InputField roomName;
    private RoomInfo[] roomslist;
    public GameObject chatMenu;
    public GameObject hud;
    public string PlayerLogin = "Steve";
    public static bool connected = false;

    string status;

    // Use this for initialization
    void Start()
    {
        Globals.isMulti = true;
        PlayerLogin = Globals.PlayerName;
        Cursor.visible = true;
        multiLobbyCanvas = GameObject.Find("MultiMenu");
        chatMenu = GameObject.Find("Chat");
        chatMenu.SetActive(false);
        hud = GameObject.Find("HUD");
        hud.SetActive(false);
        Globals.Paused = false;
        Globals.isMulti = true;
        Cursor.lockState = CursorLockMode.None;
        multiLobbyCanvas.SetActive(true);
        joinButton.interactable = false;
        hostButton.interactable = false;
        dropdown.interactable = false;
        roomName.interactable = false;
        PhotonNetwork.ConnectUsingSettings(gameVersion);
    }

    // Update is called once per frame
    void Update()
    {
        if (status != PhotonNetwork.connectionStateDetailed.ToString())
        {
            status = PhotonNetwork.connectionStateDetailed.ToString();
            statusText.text = status;
        }
        if (!connected && Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    void OnConnectedToMaster()
    {
        //Debug.Log("Connected to server");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        TypedLobby tl = new TypedLobby(SceneManager.GetActiveScene().name, LobbyType.Default);
        PhotonNetwork.JoinLobby(tl);
        joinButton.interactable = false;
        hostButton.interactable = false;
        dropdown.interactable = false;
        roomName.interactable = false;
    }

    public void Join()
    {
        if (dropdown.captionText.text == "Aléatoire")
            PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinRoom(dropdown.captionText.text);
    }

    void OnPhotonRandomJoinFailed()
    {
        connected = false;
        statusText.text = "There is no room to join...";
    }

    public void Host()
    {
        string roomn = roomName.text;
        if (roomn == "")
            statusText.text = "Veuillez entrer un nom.";
        else
        {
            PhotonNetwork.CreateRoom(roomn);
        }
    }

    void OnJoinedRoom()
    {
        chatMenu.SetActive(true);
        Destroy(GameObject.Find("Corps"));
        multiLobbyCanvas.SetActive(false);
        hud.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        Globals.MainPlayer = PhotonNetwork.Instantiate(player.name, GameObject.FindGameObjectWithTag("SpawnPoint").transform.position, Quaternion.identity, 0);
        connected = true; 
    }

    public void SwitchLevel()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex == 4 ? 5 : 4);
    }

    public void MainMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        DateTime dt = DateTime.Now;
        chatMenu.GetComponent<ChatMenu>().addMessage("[Room]" + "[" + (dt.Hour < 10 ? "0" + dt.Hour.ToString() : dt.Hour == 0 ? "00" : dt.Hour.ToString()) + ":" + (dt.Minute < 10 ? "0" + dt.Minute.ToString() : dt.Minute == 0 ? "00" : dt.Minute.ToString()) + ":" + (dt.Second < 10 ? "0" + dt.Second.ToString() : dt.Second == 0 ? "00" : dt.Second.ToString()) + "] " + newPlayer.name + " s'est connecté.");
        //Debug.Log("Player Connected");
    }

    void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        //Debug.Log("Player Disconnected");
        DateTime dt = DateTime.Now;
        chatMenu.GetComponent<ChatMenu>().addMessage("[Room]" + "[" + (dt.Hour < 10 ? "0" + dt.Hour.ToString() : dt.Hour == 0 ? "00" : dt.Hour.ToString()) + ":" + (dt.Minute < 10 ? "0" + dt.Minute.ToString() : dt.Minute == 0 ? "00" : dt.Minute.ToString()) + ":" + (dt.Second < 10 ? "0" + dt.Second.ToString() : dt.Second == 0 ? "00" : dt.Second.ToString()) + "] " + otherPlayer.name + " s'est déconnecté.");
    }

    void OnJoinedLobby()
    {
        //Debug.Log("Joined lobby");
        joinButton.interactable = true;
        hostButton.interactable = true;
        dropdown.interactable = true;
        roomName.interactable = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void OnReceivedRoomListUpdate()
    {
        refreshRooms();
    }

    private void refreshRooms()
    {
        roomslist = PhotonNetwork.GetRoomList();
        dropdown.options.Clear();
        dropdown.options.Add(new Dropdown.OptionData("Aléatoire"));
        foreach (RoomInfo r in roomslist)
        {
            dropdown.options.Add(new Dropdown.OptionData(r.name));
            //Debug.Log(r.name);
        }
    }
}
