using UnityEngine;

public class SingletonMonobehaviour<T> : MonoBehaviour where T : Component //여기서 component는 유니티의 모든 컴포넌트의 기본 클래스이다.
    // 즉 Singleton 클래스는 unity의 모든 오브젝트 타입에 대해 싱글톤 인스턴스를 생성할 수 있음을 의미한다.
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    //
    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


public class Singleton<T> where T : new()
{
    private static bool _isCreated;
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_isCreated)
                return _instance;

            _instance ??= new T();
            _isCreated = true;

            return _instance;
        }
    }
}
