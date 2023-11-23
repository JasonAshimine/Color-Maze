using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : Singleton<LightController>
{
    public Light2D Left;
    public Light2D Middle;
    public Light2D Right;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void LeftLight(Color c)
    {
        Left.color = c;
    }


}
