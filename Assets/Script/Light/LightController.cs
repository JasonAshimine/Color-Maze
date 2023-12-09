using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;
using Variable;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [Range(0f, 1f)] public float minIntensity;

    [SerializeField] private ColorDirection _colorData;

    [SerializeField] private Light2D Left;
    [SerializeField] private Light2D Middle;
    [SerializeField] private Light2D Right;


    void Start()
    {
        resizeBackground(); 
    }

    public void updateAllLights(object _data)
    {
        LightEventData data = (LightEventData)_data;

        updateLight(Left, data.left);
        updateLight(Middle, data.middle);
        updateLight(Right, data.right);
    }

    public void updateLight(Light2D light, ColorIntensity data)
    {
        light.color = data.color * CalcDistanceIntensity(data.intensity);
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

public struct LightEventData
{
    public LightEventData(ColorIntensity left, ColorIntensity middle,  ColorIntensity right)
    {
        this.left = left;
        this.middle = middle;
        this.right = right;
    }

    public ColorIntensity left;
    public ColorIntensity middle;
    public ColorIntensity right;
}