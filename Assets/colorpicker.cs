using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class colorpicker : MonoBehaviour, IPointerClickHandler
{

    public Color output;
    public void OnPointerClick(PointerEventData eventData)
    {
        output = Pick(Camera.main.WorldToScreenPoint(eventData.position), GetComponent<Image>());
    }

    Color Pick(Vector2 screenPoint, Image imagetopick)
    {
        Vector2 point;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imagetopick.rectTransform, screenPoint, Camera.main, out point);
        point += imagetopick.rectTransform.sizeDelta / 2;
        Texture2D t = GetComponent<Image>().sprite.texture;
        Vector2Int m_point = new Vector2Int((int)((t.width * point.x) / imagetopick.rectTransform.sizeDelta.x), (int)((t.height * point.y) / imagetopick.rectTransform.sizeDelta.y));
        return t.GetPixel(m_point.x, m_point.y);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
