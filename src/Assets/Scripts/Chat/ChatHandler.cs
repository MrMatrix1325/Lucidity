using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.Chat;
using ExitGames.Client.Photon;

public class ChatHandler : MonoBehaviour, IChatClientListener
{

    ChatHandler instance;
    ChatClient chatclient;
    ExitGames.Client.Photon.Chat.AuthenticationValues authValues;
    string ChatAppId = "d0b0aa14-8677-4acd-b1e6-3ee003e87e05";
    string appVersion = "0.2";
    string Username;
    bool SubscribedtoRoom = false;
    string RoomChannelName;
    bool Connected;
    Queue<string> Messages = new Queue<string>(9);
    string LastPrivateMessageSender;
    NetworkController nc;
    public ChatMenu chatMenu;

    public void DebugReturn(DebugLevel level, string message)
    {
        //Debug.Log("DebugReturn: " + level + ", message: " + message);
    }

    public void OnChatStateChange(ChatState state)
    {
        //Debug.Log("Chat state has changed: " + state);
    }

    public void OnConnected()
    {
        Connected = true;
        chatclient.Subscribe(new string[] { "Global" });
        chatclient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnDisconnected()
    {
        chatclient.SetOnlineStatus(ChatUserStatus.Offline);
        //Debug.Log("You have been disconnected: " + chatclient.DisconnectedCause);
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        int length = senders.Length;
        DateTime dt = DateTime.Now;
        for (int i = 0; i < length; i++)
        {
            string message = "[" + GetReadableChannelName(channelName) + "]" + "[" + (dt.Hour < 10 ? "0" + dt.Hour.ToString() : dt.Hour == 0 ? "00" : dt.Hour.ToString()) + ":" + (dt.Minute < 10 ? "0" + dt.Minute.ToString() : dt.Minute == 0 ? "00" : dt.Minute.ToString()) + ":" + (dt.Second < 10 ? "0" + dt.Second.ToString() : dt.Second == 0 ? "00" : dt.Second.ToString()) + "] " + senders[i] + ": " + messages[i];
            chatMenu.addMessage(message);
        }

    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        chatMenu.addMessage("[" + GetReadableChannelName(channelName) + "]" + sender + ": " + message);
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        //Debug.Log("User: " + user + ", status: " + status + ", got a message: " + gotMessage + ", message" + message);
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        string strin = "Subscribed to: ";
        foreach (string str in channels)
        {
            strin += str + ", ";
        }
        //Debug.Log(strin);
    }

    public void OnUnsubscribed(string[] channels)
    {
        string strin = "Unsubscribed to: ";
        foreach (string str in channels)
        {
            strin += str + ", ";
        }
        //Debug.Log(strin);
    }

    public void Connect()
    {
        chatclient.Connect(ChatAppId, appVersion, authValues);
    }

    void JoinRoomChannels()
    {
        SubscribedtoRoom = true;
        if (PhotonNetwork.room != null)
        {
            RoomChannelName = "Room - " + PhotonNetwork.room.name;
            chatclient.Subscribe(new string[] { RoomChannelName });
            chatMenu.channelsList.Add(RoomChannelName);
        }
    }

    void LeaveRoomChannels()
    {
        if (RoomChannelName != null)
        {
            chatclient.Unsubscribe(new string[] { RoomChannelName });
        }
        SubscribedtoRoom = false;
    }

    public void LeaveAllChannels()
    {
        chatclient.Unsubscribe(new string[] { "Global", RoomChannelName });
    }

    public void SetOnlineStatus(int status, string message)
    {
        chatclient.SetOnlineStatus(status, message);
    }

    public bool IsConnected()
    {
        return Connected;
    }

    public void SendText(string text, string channel, bool isPrivateMessage)
    {
        if (text == "")
        {
            return;
        }

        if (isPrivateMessage)
        {
            chatclient.SendPrivateMessage(channel, text);
        }
        else
        {
            chatclient.PublishMessage(channel, text);
        }
    }

    public Queue<string> GetMessages()
    {
        return Messages;
    }

    public string GetReadableChannelName(string channel)
    {
        if (channel == RoomChannelName)
        {
            return "Room";
        }
        else
        {
            return "Global";
        }
    }
    public string GetRoomChannelName()
    {
        return RoomChannelName;
    }

    public string LastMessageSender()
    {
        return LastPrivateMessageSender;
    }

    public void OnApplicationQuit()
    {
        if (chatclient != null)
        {
            chatclient.Disconnect();
        }
    }


    // Use this for initialization
    void Start()
    {
        enabled = gameObject.GetComponent<PhotonView>().isMine;
        if (!enabled)
            return;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        nc = GameObject.Find("NetworkHolder").GetComponent<NetworkController>();
        chatMenu = GameObject.Find("Chat").GetComponent<ChatMenu>();
        chatMenu.ch = this;
        Username = nc.PlayerLogin;
        ConnectionProtocol connectProtocol = ConnectionProtocol.Udp;
        chatclient = new ChatClient(this, connectProtocol);
        chatclient.ChatRegion = "EU";
        authValues = new ExitGames.Client.Photon.Chat.AuthenticationValues();
        authValues.UserId = Username;
        authValues.AuthType = ExitGames.Client.Photon.Chat.CustomAuthenticationType.None;
        instance = this;
        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true;
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled)
            return;
        if (!SubscribedtoRoom && Connected)
        {
            JoinRoomChannels();
        }
        if (chatclient != null)
        {
            chatclient.Service();
        }
    }
}