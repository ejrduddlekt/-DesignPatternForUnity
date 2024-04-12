using System.Text;
using UnityEngine;
using UnityEngine.Pool;

//// - Ư��
/*CountActive
    Ǯ���� �����Ǿ����� ���� ��� ���̸� ���� ��ȯ���� ���� ��ü�� ���Դϴ�.
 */
/*CountAll
    Ȱ�� �� ��Ȱ�� ��ü�� �� ���Դϴ�.
 */
/* CountInactive
    Ǯ���� ���� ����� �� �ִ� ��ü ���Դϴ�.

 */

//// - Constructors
/* ObjectPool_1
    Creates a new ObjectPool instance.
 */

//// - Public Methods
/*Clear
 *      Ǯ���� ��� �׸��� �����մϴ�. Ǯ�� �ı� �ݹ��� ���Ե� ��� Ǯ�� �ִ� �� �׸� ���� ȣ��˴ϴ�.
 */
/*Dispose
 *     Ǯ���� ��� �׸��� �����մϴ�.Ǯ�� �ı� �ݹ��� ���Ե� ��� Ǯ�� �ִ� �� �׸� ���� ȣ��˴ϴ�.
 */
/*Get
 *     Ǯ���� �ν��Ͻ��� �����ɴϴ�. Ǯ�� ��� ������ �� �ν��Ͻ��� �����˴ϴ�.
 */
/*Release
 *     �ν��Ͻ��� Ǯ�� �ٽ� ��ȯ�մϴ�. �ν��Ͻ��� ���� �� Ǯ�� ��ȯ�ϸ� �ν��Ͻ��� �ı��˴ϴ�.
 */


// OnParticleSystemStopped �̺�Ʈ�� ���ŵǸ� �� ���� ��Ҵ� ��ƼŬ �ý����� Ǯ�� ��ȯ�մϴ�.
[RequireComponent(typeof(ParticleSystem))]
public class ReturnToPool : MonoBehaviour
{
    public ParticleSystem system;
    public IObjectPool<ParticleSystem> pool;

    void Start()
    {
        system = GetComponent<ParticleSystem>();
        var main = system.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        // Return to the pool
        pool.Release(system);
    }
}

// �� ������ ���� �ý����� ������ �� �ֵ��� Ǯ�� ����ϴ� ������ ���� ParticleSystem�� ���� �ֽ��ϴ�.
public class PoolExample : MonoBehaviour
{
    public enum PoolType
    {
        Stack,
        LinkedList
    }

    public PoolType poolType;

    // �̹� Ǯ�� �ִ� �׸��� �������Ϸ��� �ϸ� ���� �˻翡�� ������ �߻��մϴ�.
    public bool collectionChecks = true;
    public int maxPoolSize = 10;

    IObjectPool<ParticleSystem> m_Pool;

    public IObjectPool<ParticleSystem> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                if (poolType == PoolType.Stack)
                    m_Pool = new ObjectPool<ParticleSystem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                else
                    m_Pool = new LinkedPool<ParticleSystem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
            }
            return m_Pool;
        }
    }

    ParticleSystem CreatePooledItem()
    {
        var go = new GameObject("Pooled Particle System");
        var ps = go.AddComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var main = ps.main;
        main.duration = 1;
        main.startLifetime = 1;
        main.loop = false;

        // ParticleSystems�� �����Ǹ� Pool�� �ǵ����� �� ���˴ϴ�.
        var returnToPool = go.AddComponent<ReturnToPool>();
        returnToPool.pool = Pool;

        return ps;
    }

    // �������� ����Ͽ� �׸��� Ǯ�� ��ȯ�� �� ȣ��˴ϴ�
    void OnReturnedToPool(ParticleSystem system)
    {
        system.gameObject.SetActive(false);
    }

    // Get�� ����Ͽ� Ǯ���� �׸��� ������ �� ȣ��˴ϴ�
    void OnTakeFromPool(ParticleSystem system)
    {
        system.gameObject.SetActive(true);
    }

    // Ǯ �뷮�� �����ϸ� ��ȯ�Ǵ� ��� �׸��� �ı��˴ϴ�.
    // �ı� �ൿ�� ������ �ϴ��� ������ �� �ֽ��ϴ�. ���⼭ ���ӿ�����Ʈ�� �ı��մϴ�.
    void OnDestroyPoolObject(ParticleSystem system)
    {
        Destroy(system.gameObject);
    }

    void OnGUI()
    {
        GUILayout.Label("Pool size: " + Pool.CountInactive);
        if (GUILayout.Button("Create Particles"))
        {
            var amount = Random.Range(1, 10);
            for (int i = 0; i < amount; ++i)
            {
                var ps = Pool.Get();
                ps.transform.position = Random.insideUnitSphere * 10;
                ps.Play();
            }
        }
    }
}

