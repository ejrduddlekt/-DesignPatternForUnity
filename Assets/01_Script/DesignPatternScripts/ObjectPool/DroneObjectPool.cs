using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DroneObjectPool : MonoBehaviour
{
    public int maxPoolSize = 10; //풀에 보관할 인스턴스의 최대 개수를 설정

    //기본 스택 크기 설정, 이것은 드론 인스턴스를 저장하는 데 사용할 스택 데이터 구조체와 관련된 속성이다.
    //현재는 크게 중요하지 않으니 무시해도 된다.
    public int stackDefaultCapacity = 10;


    private IObjectPool<Drone> _pool;
    public IObjectPool<Drone> Pool
    {
        get
        {
            if (_pool == null)
                _pool = new ObjectPool<Drone>(
                    CreatedPooledItem,
                    OnTakeFromPool,
                    OnReturnToPool,
                    OnDestroyPoolObject,
                    true,
                    stackDefaultCapacity,
                    maxPoolSize
                    );

            return _pool;
        }
    }

    

    private Drone CreatedPooledItem()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        Drone drone = go.AddComponent<Drone>();

        go.name = "Drone";
        drone.Pool = Pool;

        return drone;
    }

    private void OnReturnToPool(Drone drone)
    {
        drone.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Drone drone)
    {
        drone.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(Drone drone)
    {
        Destroy(drone.gameObject);
    }

    public void Spawn()
    {
        var amount = Random.Range(1, 10);

        for (int i = 0; i < amount; ++i)
        {
            var drone = Pool.Get();

            drone.transform.position = Random.insideUnitSphere * 10;
        }
    }
}
