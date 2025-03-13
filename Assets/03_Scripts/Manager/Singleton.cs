using UnityEngine;

/// <summary>
/// 컴포넌트 제너릭 형식의 싱글톤 클래스
/// </summary>
/// <typeparam name="T">MonoBehaviour</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    
    public static T Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = FindObjectOfType<T>();
            if (instance != null) return instance;
            var obj = new GameObject(typeof(T).Name);
            instance = obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}