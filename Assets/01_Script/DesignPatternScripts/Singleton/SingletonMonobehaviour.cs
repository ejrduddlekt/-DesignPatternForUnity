using UnityEngine;

public class SingletonMonobehaviour<T> : MonoBehaviour where T : Component //���⼭ component�� ����Ƽ�� ��� ������Ʈ�� �⺻ Ŭ�����̴�.
    // �� Singleton Ŭ������ unity�� ��� ������Ʈ Ÿ�Կ� ���� �̱��� �ν��Ͻ��� ������ �� ������ �ǹ��Ѵ�.
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
