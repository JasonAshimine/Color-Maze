using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SideLightMenu : MonoBehaviour
{
    public UnityEvent<colorTypes, Color> ColorMenuEvent;

    [SerializeField] private Image Left;
    [SerializeField] private Image Right;


    void Start()
    {
        
    }

    public Image getImage(colorTypes id)
    {
        switch (id)
        {
            case colorTypes.Left: return Left;
            case colorTypes.Right: return Right;
            default:
                return null;
        }
    }


    public void updateColor(colorTypes id, Color color)
    {
        Image image = getImage(id);
        image.color = color;
    }
}
