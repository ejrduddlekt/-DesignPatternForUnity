using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DroneObjectPool : MonoBehaviour
{
    public int maxPoolSize = 10; //Ǯ�� ������ �ν��Ͻ��� �ִ� ������ ����

    //�⺻ ���� ũ�� ����, �̰��� ��� �ν��Ͻ��� �����ϴ� �� ����� ���� ������ ����ü�� ���õ� �Ӽ��̴�.
    //����� ũ�� �߿����� ������ �����ص� �ȴ�.
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
