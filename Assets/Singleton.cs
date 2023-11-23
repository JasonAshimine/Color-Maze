using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _Instance;
    public static T Instance
    {
        get
        {
            if(_Instance)
                return _Instance;

            return GameObject.FindAnyObjectByType<T>();
        } 
        
        protected set
        {
            _Instance = value;
        }
    }
}
