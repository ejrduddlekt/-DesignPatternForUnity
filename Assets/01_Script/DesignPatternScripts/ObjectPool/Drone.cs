using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Drone : MonoBehaviour
{
    public IObjectPool<Drone> Pool {  get; set; }

    public float _currentHealth;

    [SerializeField]
    private float maxHealth = 100.0f;

    [SerializeField]
    private float timeToSelfDestruct = 3.0f;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        AttackPlayer();
        StartCoroutine(SelfDestruct());
    }


    //오브젝트 초기화
    private void OnDisable()
    {
        ResetDrone();
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToSelfDestruct);
        TakeDamage(maxHealth);
    }

    private void ReturnToPool()
    {
        Pool.Release(this);
    }

    //드론이 플레이어를 찾아 공격한다는 내용이지만 세부적인건 구현하지 않았다.
    public void AttackPlayer()
    {
        Debug.Log("Attack player!");
    }

    //매개변수 만큼 데미지를 입으며, 0이하가 되면 풀로 돌아간다.
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0f)
            ReturnToPool();
    }

    //초기상태로 되돌린다.
    public void ResetDrone()
    {
        _currentHealth = maxHealth;
    }
}





