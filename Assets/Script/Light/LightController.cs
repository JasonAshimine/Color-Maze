using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;
using Variable;
using UnityEngine.Rendering.Universal;


public enum LightEventType
{
    Toggle,
    Color,
    ChangeLayer,
}

public class LightController : MonoBehaviour
{
    [Range(0f, 1f)] public float minIntensity = 0.5f;

    [SerializeField] private ColorDirection _colorData;
    [SerializeField] private LightDataSet _lightData;

    [SerializeField] private Light2D Left;
    [SerializeField] private Light2D Middle;
    [SerializeField] private Light2D Right;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        resizeBackground();
        _lightData.Reset();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void updateAllLights(object data)
    {
        switch((LightEventType)data)
        {
            case LightEventType.Color:
                updateLight(Left, _lightData.Left);
                updateLight(Middle, _lightData.Middle);
                updateLight(Right, _lightData.Right);
                break;
            case LightEventType.Toggle:
                Left.gameObject.SetActive(_lightData.toggleLeft);
                Middle.gameObject.SetActive(_lightData.toggleMiddle);
                Right.gameObject.SetActive(_lightData.toggleRight);
                break;
            case LightEventType.ChangeLayer:
                toggleRenderLayer();
                break;
        }
    }

    private void toggleRenderLayer()
    {
        if (_spriteRenderer.sortingOrder == 0)
            _spriteRenderer.sortingOrder = 1;
        else
            _spriteRenderer.sortingOrder = 0;
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