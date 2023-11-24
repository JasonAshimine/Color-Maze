using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class LightController : Singleton<LightController>
{
    public UnityEvent<colorTypes, Color> OnChange;

    [Range(0f, 1f)] public float minIntensity; 

    public Light2D Left;
    public Light2D Middle;
    public Light2D Right;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        resizeBackground(); 
    }

    public void updateLeft(Color color, float intesity)
    {
        Left.color = color * CalcDistanceIntensity(intesity);
        OnChange.Invoke(colorTypes.Left, Left.color);
    }
    public void updateMiddle(Color color, float intesity)
    {
        Middle.color = color * CalcDistanceIntensity(intesity);
        OnChange.Invoke(colorTypes.Center, Middle.color);
    }
    public void updateRight(Color color, float intesity)
    {
        Right.color = color * CalcDistanceIntensity(intesity);
        OnChange.Invoke(colorTypes.Right, Right.color);
    }

    private float CalcDistanceIntensity(float distance)
    {
        return Mathf.Lerp(minIntensity, 1f, (1 - Mathf.Pow(0.7f, distance)));
    }
    private void resizeBackground()
    {
        Vector2 worldScale = Camera.main.ViewportToWorldPoint(Vector3.one);
        transform.localScale = worldScale * 2;
    }
}
