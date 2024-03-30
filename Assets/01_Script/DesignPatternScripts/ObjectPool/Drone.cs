using System.Collections;
using System.Collections.Generic;
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

    public void ResetDrone()
    {
        _currentHealth = maxHealth;
    }

    public void AttackPlayer()
    {
        Debug.Log("Attack player!");
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0f)
            ReturnToPool();
    }
}
