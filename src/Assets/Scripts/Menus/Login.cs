using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

    public Text statusTextLogin;
    public Toggle remember;
    public InputField LoginField;
    public InputField PassField;
    public GameObject MainCanvas;
    public GameObject canvas;
    public int MultiSceneID = 4;
    private bool LoggedIn = false;

    // Use this for initialization
    void Start () {
        //MainCanvas = GameObject.Find("MainMenu");
        canvas = GameObject.Find("MultiLogin");
        PassField.inputType = InputField.InputType.Password;
        if (PlayerPrefs.HasKey("Login"))
            LoginField.text = PlayerPrefs.GetString("Login");
        if (PlayerPrefs.HasKey("Password"))
            PassField.text = PlayerPrefs.GetString("Password");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoginB()
    {
        string login = LoginField.text;
        string pass = PassField.text;
        if (remember.isOn)
        {
            PlayerPrefs.SetString("Login", login);
            PlayerPrefs.SetString("Password", pass);
        }
        else
        {
            if (PlayerPrefs.HasKey("Password"))
                PlayerPrefs.DeleteKey("Password");
        }
        if (login == "" || pass == "")
        {
            statusTextLogin.text = "Champs incomplets";
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("username", login);
            form.AddField("password", pass);
            WWW w = new WWW("http://lucidity.fr/UnityLogin.php", form);
            statusTextLogin.text = "Connexion en cours...";
            StartCoroutine(LoginSQL(w, login));
        }
    }

    public void Back()
    {
        MainCanvas.SetActive(true);
        canvas.SetActive(false);
    }

    IEnumerator LoginSQL(WWW w, string login)
    {
        yield return w;
        if (w.error == null)
        {
            if (w.text.Contains(login))
            {
                Globals.PlayerName = login;
                LoggedIn = true;
                PhotonNetwork.playerName = login;
                statusTextLogin.text = "Success :)";
                SceneManager.LoadScene(MultiSceneID);
                //PhotonNetwork.ConnectUsingSettings(gameVersion);
            }
            else
                statusTextLogin.text = "Incorrect";
        }
        else
        {
            statusTextLogin.text = "Connexion serveur échouée";
        }
    }
}
