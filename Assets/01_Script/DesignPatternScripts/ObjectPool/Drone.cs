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


    //������Ʈ �ʱ�ȭ
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

    //����� �÷��̾ ã�� �����Ѵٴ� ���������� �������ΰ� �������� �ʾҴ�.
    public void AttackPlayer()
    {
        Debug.Log("Attack player!");
    }

    //�Ű����� ��ŭ �������� ������, 0���ϰ� �Ǹ� Ǯ�� ���ư���.
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0f)
            ReturnToPool();
    }

    //�ʱ���·� �ǵ�����.
    public void ResetDrone()
    {
        _currentHealth = maxHealth;
    }
}





