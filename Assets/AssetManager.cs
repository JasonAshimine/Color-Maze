using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : Singleton<AssetManager>
{
    public GameObject PlayerPrefab;
   
    void Start()
    {
        Instance = this;

        
    }


    public GameObject Spawn(GameObject obj)
    {


        new GameObject("container");
        return obj;
    }
}
