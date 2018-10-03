using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChatMenu : MonoBehaviour
{

    public InputField msgInput;
    public Text defaultText;
    public GameObject messagesPanel;
    public Button SendButton;
    public int MAX_MESSAGES = 9;
    private bool isWriting = false;
    private int channelId = 0;
    public List<string> channelsList = new List<string>();
    public ChatHandler ch;
    private Color hiddenCol = new Color(1f, 1f, 1f, 0f);
    private Color visibleCol = new Color(1f, 1f, 1f, 0.3f);
    private bool frozenRotY = false;

    void Start()
    {
        messagesPanel = GameObject.Find("MessagesPanel");
        channelsList.Add("Global");
        disableWriting();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {
        if (Input.GetButtonDown("Chat"))
        {
            enableWriting();
        }
        if (isWriting)
        {
            if (Input.GetButtonDown("Submit"))
            {
                SendMessage();
            }
            if (Input.GetButtonDown("Cancel"))
                disableWriting();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                channelId = (channelId + 1) % channelsList.Count;
                //Debug.Log(channelsList[channelId]);
                msgInput.placeholder.GetComponent<Text>().text = "[" + (channelsList[channelId][0] == 'R' ? "Room" : "Global") + "] Envoyer un message";
            }
        }
    }

    public void addMessage(string message)
    {
        Text newMsg = Instantiate(defaultText);
        newMsg.text = message;
        if (newMsg.text[1] == 'G')
        {
            newMsg.color = new Color(0.5f, 0f, 1f);
        }
        else
        {
            newMsg.color = new Color(0.01568627450980392156862745098039f, 0.97254901960784313725490196078431f, 0.10196078431372549019607843137255f);
        }
        newMsg.transform.SetParent(messagesPanel.transform);
        if (messagesPanel.transform.childCount > MAX_MESSAGES)
            Destroy(messagesPanel.transform.GetChild(0).gameObject);
    }

    private void SendMessage()
    {
        string msg = msgInput.text;
        if (msg[0] == '#')
        {
            addMessage(Command(msg));
            disableWriting();
        }
        else
        {
            ch.SendText(msg, channelsList[channelId], false);
            disableWriting();
        }
    }

    private void SendMessage(string msg)
    {
        ch.SendText(msg, "Global", false);
        disableWriting();
    }

    private void enableWriting()
    {
        Globals.Paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        msgInput.interactable = true;
        msgInput.Select();
        msgInput.ActivateInputField();
        isWriting = true;
        messagesPanel.GetComponent<Image>().color = visibleCol;
        SendButton.interactable = true;
    }

    private void disableWriting()
    {
        Globals.Paused = false;
        msgInput.text = "";
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isWriting = false;
        messagesPanel.GetComponent<Image>().color = hiddenCol;
        SendButton.interactable = false;
        msgInput.interactable = false;
    }

    private string Command(string input)
    {
        switch (input)
        {
            case "#fly":
                if (frozenRotY)
                {
                    Globals.MainPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    frozenRotY = false;
                }
                else
                {
                    Globals.MainPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
                    frozenRotY = true;
                }
                return frozenRotY ? "Tu voles" : "Tu ne voles plus";
            case "#ping":
                return "Pong!";
            case "#lucidity":
                Globals.MainPlayer.GetComponent<Stuff>().lucidity_stock = 100;
                return "Cadeau";
            case "#all":
                ch.LeaveAllChannels();
                return "Disconnected from all channels";
            case "#rigid":
                Globals.MainPlayer.GetComponent<CapsuleCollider>().enabled = !Globals.MainPlayer.GetComponent<CapsuleCollider>().enabled;
                return "Disconnected from all channels";
            default:
                return "Commande inconnue.";
        }
    }
}