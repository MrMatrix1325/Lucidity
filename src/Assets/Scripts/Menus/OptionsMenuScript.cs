using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour {

    // Use this for initialization
    public GameObject MainCanvas;
    public GameObject OptionsCanvas;
    public Slider SensitivityBar;
    public Slider VolumeBar;
    public Text SensitivityText;
    public Text VolumeText;

    void Start () {
        //inCanvas = GameObject.Find("PauseMenu");
        OptionsCanvas = GameObject.Find("OptionsMenu");
        if (PlayerPrefs.HasKey("Sensitivity"))
            SensitivityBar.value = PlayerPrefs.GetFloat("Sensitivity");
        if (PlayerPrefs.HasKey("Volume"))
        {
            VolumeBar.value = PlayerPrefs.GetFloat("Volume");
        }
        //Apply();
        SensitivityText.text = SensitivityBar.value.ToString();
        VolumeText.text = VolumeBar.value.ToString();
        setvolume();

    }
	
	// Update is called once per frame
	void Update () {
        SensitivityText.text = SensitivityBar.value.ToString();
        VolumeText.text = VolumeBar.value.ToString();
        setvolume();
        //Apply();
    }

    public void ReturnMain()
    {
        MainCanvas.SetActive(true);
        OptionsCanvas.SetActive(false);    
    }

    public void Apply()
    {
        PlayerPrefs.SetFloat("Sensitivity", SensitivityBar.value);
        PlayerPrefs.SetFloat("Volume", VolumeBar.value);
        PlayerPrefs.Save();
    }

    public void setvolume()
    {
        AudioListener.volume = VolumeBar.value / 100;
    }

    public float getvolume()
    {
        return VolumeBar.value / 100;
    }
}
