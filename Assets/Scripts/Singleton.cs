using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    private static T _instance = null;

    private bool _alive = true;
    private bool _dontDestroy;

    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                T[] managers = FindObjectsOfType<T>();
                if (managers != null)
                {
                    if (managers.Length == 1)
                    {
                        _instance = managers[0];
                        return _instance;
                    }
                    else
                    {
                        if (managers.Length > 1)
                        {
                            for (int i = 0; i < managers.Length; i++)
                            {
                                if(managers[i] != _instance)
                                    Destroy(managers[i].gameObject);
                            }
                            Debug.LogError($"Have more that one {typeof(T).Name} in scene. " +
                                            "But this is Singleton! Check project.");
                        }
                    }
                }
                return _instance;
            }
        }

        set
        {
            _instance = value as T;
        }
    }

    public static bool IsAlive
    {
        get
        {
            if (_instance == null)
                return false;
            return _instance._alive;
        }
    }

    protected virtual void Awake()
    {
        _instance = this as T;
    }

    protected void OnDestroy() { _alive = false; }

    protected void OnApplicationQuit() { _alive = false; }

    protected virtual void Initialization(bool isDontDestroy)
    {
        if (isDontDestroy)
        {
            _dontDestroy = true;
            DontDestroyOnLoad(this);
        }
    }
}
