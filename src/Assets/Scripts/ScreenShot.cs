using UnityEngine;
using System;
using System.Collections;

public class ScreenShot : MonoBehaviour
{

    public RenderTexture overviewTexture;
    public GameObject OVcamera;
    public UnityEngine.Camera camOV;
    public string path;
    public int count;
    public int count_max = 30;
    public bool begin = false;
    public GameObject Po;
    public Animation anim;

    public IEnumerator TakeScreenShot()
    {
        yield return new WaitForEndOfFrame();
        camOV = OVcamera.GetComponent<UnityEngine.Camera>();
        RenderTexture currentRT = RenderTexture.active;

        RenderTexture.active = camOV.targetTexture;
        camOV.Render();
        Texture2D imageOverview = new Texture2D(camOV.targetTexture.width, camOV.targetTexture.height, TextureFormat.RGB24, false);
        imageOverview.ReadPixels(new Rect(0, 0, camOV.targetTexture.width, camOV.targetTexture.height), 0, 0);
        imageOverview.Apply();
        RenderTexture.active = currentRT;


        // Encode texture into PNG
        byte[] bytes = imageOverview.EncodeToPNG();

        // save in memory
        System.IO.File.WriteAllBytes(path + count + ".png" , bytes);
    }

    // Use this for initialization
    void Start()
    {
        OVcamera = GameObject.FindGameObjectWithTag("OverviewCamera");
        path = Application.persistentDataPath + "/Level 1/Video_";
        count = 0;
        begin = false;
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown("f9"))
        {
            //begin = true;
            anim.Play("Camera");
        }
        if (count == 20)
        {
            //Po.transform.localEulerAngles.y = 90;
        }
        if (begin && count < count_max)
        {
            StartCoroutine(TakeScreenShot());
        }
        count++;
    }
}
