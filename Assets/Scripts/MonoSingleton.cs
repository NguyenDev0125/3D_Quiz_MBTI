using Unity.VisualScripting;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool isDontDestroy;
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>(true);
                if (instance == null) Debug.Log($"{typeof(T)} null");
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        if(isDontDestroy) DontDestroyOnLoad(this.gameObject);
    }
}
