using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.Events;

[Serializable]
public class colorevent : UnityEvent<Color>
{

}
public class colorpicker2 : MonoBehaviour
{
    public TextMeshProUGUI DebugText;
    public colorevent OnColorPreview;
    public colorevent OnColorSelect;
    RectTransform Rect;
    Texture2D ColorTexture;

    void Start()
    {
        Rect = GetComponent<RectTransform>();
        ColorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, null, out delta);
        string debug = "mousePosition = " + Input.mousePosition;
        debug += "<br>delta = " + delta;

        float width = Rect.rect.width;
        float height = Rect.rect.height;
        delta += new Vector2(width * .5f, height * .5f);
        debug += "<br>offset delta=" + delta;

        float x = Mathf.Clamp(delta.x / width, 0f, 1f);
        float y = Mathf.Clamp(delta.y / height, 0f, 1f);
        debug += "<br>x = " + x + " y =" + y;

        int texX = Mathf.RoundToInt(x * ColorTexture.width);
        int texY = Mathf.RoundToInt(y * ColorTexture.width);
        debug += "<br>texX = " + texX + " texY =" + texY;

        Color color = ColorTexture.GetPixel(texX, texY);

        print(color);
        Debug.Log(color);

        //DebugText.color = color;
        DebugText.text = debug;

        OnColorPreview?.Invoke(color);

        if (Input.GetMouseButtonDown(0))
        {
            OnColorSelect?.Invoke(color);
        }
    }
}
