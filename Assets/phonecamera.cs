using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phonecamera : MonoBehaviour
{

    private bool camavailable;
    private WebCamTexture backcam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0) //no hay ninguna camara
        {
            Debug.Log("ninguna camara detectada");
            camavailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++) //si el dispositivo tiene mas de una camara
        {
            if (!devices[i].isFrontFacing)
            {
                backcam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (backcam == null)
        {
            Debug.Log("no es posible encontrar la camara");
            return;
        }

        backcam.Play();
        background.texture = backcam;

        camavailable = true;
    }
    private void Update()
    {
        if (!camavailable)
            return;

        float ratio = (float)backcam.width / (float)backcam.height;
        fit.aspectRatio = ratio;

        float scaleY = backcam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backcam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}


