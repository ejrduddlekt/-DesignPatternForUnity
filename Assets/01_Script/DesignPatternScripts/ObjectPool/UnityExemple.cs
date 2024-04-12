using System.Text;
using UnityEngine;
using UnityEngine.Pool;

//// - 특성
/*CountActive
    풀에서 생성되었지만 현재 사용 중이며 아직 반환되지 않은 개체의 수입니다.
 */
/*CountAll
    활성 및 비활성 개체의 총 수입니다.
 */
/* CountInactive
    풀에서 현재 사용할 수 있는 개체 수입니다.

 */

//// - Constructors
/* ObjectPool_1
    Creates a new ObjectPool instance.
 */

//// - Public Methods
/*Clear
 *      풀링된 모든 항목을 제거합니다. 풀에 파괴 콜백이 포함된 경우 풀에 있는 각 항목에 대해 호출됩니다.
 */
/*Dispose
 *     풀링된 모든 항목을 제거합니다.풀에 파기 콜백이 포함된 경우 풀에 있는 각 항목에 대해 호출됩니다.
 */
/*Get
 *     풀에서 인스턴스를 가져옵니다. 풀이 비어 있으면 새 인스턴스가 생성됩니다.
 */
/*Release
 *     인스턴스를 풀로 다시 반환합니다. 인스턴스를 가득 찬 풀로 반환하면 인스턴스가 파괴됩니다.
 */


// OnParticleSystemStopped 이벤트가 수신되면 이 구성 요소는 파티클 시스템을 풀로 반환합니다.
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

// 이 예제는 이전 시스템을 재사용할 수 있도록 풀을 사용하는 임의의 수의 ParticleSystem에 걸쳐 있습니다.
public class PoolExample : MonoBehaviour
{
    public enum PoolType
    {
        Stack,
        LinkedList
    }

    public PoolType poolType;

    // 이미 풀에 있는 항목을 릴리스하려고 하면 수집 검사에서 오류가 발생합니다.
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

        // ParticleSystems가 중지되면 Pool로 되돌리는 데 사용됩니다.
        var returnToPool = go.AddComponent<ReturnToPool>();
        returnToPool.pool = Pool;

        return ps;
    }

    // 릴리스를 사용하여 항목이 풀로 반환될 때 호출됩니다
    void OnReturnedToPool(ParticleSystem system)
    {
        system.gameObject.SetActive(false);
    }

    // Get을 사용하여 풀에서 항목을 가져올 때 호출됩니다
    void OnTakeFromPool(ParticleSystem system)
    {
        system.gameObject.SetActive(true);
    }

    // 풀 용량에 도달하면 반환되는 모든 항목이 파괴됩니다.
    // 파괴 행동이 무엇을 하는지 제어할 수 있습니다. 여기서 게임오브젝트를 파괴합니다.
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

