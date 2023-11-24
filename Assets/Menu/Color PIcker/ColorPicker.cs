using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public UnityEvent<Color> ColorPickerEvent;

    [SerializeField] Texture2D ColorChart;
    [SerializeField] RectTransform Chart;

    [SerializeField] RectTransform Cursor;
    [SerializeField] Image CursorColor;

    public void PickColor(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;

        Cursor.position = pointer.position;

        //Convert position to Texture position
        int x = (int)((Cursor.localPosition.x + Chart.rect.width / 2) * (ColorChart.width / Chart.rect.width));
        int y = (int)((Cursor.localPosition.y + Chart.rect.height / 2) * (ColorChart.height / Chart.rect.height));

        Color pickedColor = ColorChart.GetPixel(x, y);

        CursorColor.color = pickedColor;
        ColorPickerEvent.Invoke(pickedColor);
    }
}

