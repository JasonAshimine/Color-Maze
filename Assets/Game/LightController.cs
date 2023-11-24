using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : Singleton<LightController>
{
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
    }
    public void updateMiddle(Color color, float intesity)
    {
        Middle.color = color * CalcDistanceIntensity(intesity);
    }
    public void updateRight(Color color, float intesity)
    {
        Right.color = color * CalcDistanceIntensity(intesity);
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
