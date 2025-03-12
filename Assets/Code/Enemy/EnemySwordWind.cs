using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySwordWind : MonoBehaviour
{
    public float maxSpeed = 20f;  // �ִ� �ӵ�
    public float acceleration = 200f;  // ���ӵ�
    private float speed;// = 0f;  // ���� �ӵ�
    private ObjectPool<EnemySwordWind> pool;

    public void Setpool(ObjectPool<EnemySwordWind> WindPool)
    {
        pool = WindPool;
    }

    private void OnEnable()
    {
        speed = 0f;
        // ���� �ð� �� �ڵ� ��ȯ
        Invoke(nameof(ReturnToPool), 2f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Update()
    {
        Invoke(nameof(Move), 0.2f);

        
    }

    void Move()
    {
        //transform.Translate(Vector3.forward * 3 * Time.deltaTime);
        speed += acceleration * Time.deltaTime; // ���ӵ� ���� (���� ������)
        speed = Mathf.Min(speed, maxSpeed); // �ִ� �ӵ� ����
        transform.Translate(Vector3.forward * speed * speed * Time.deltaTime);
    }

    public void ReturnToPool()
    {
        pool.Release(this); // ���Ÿ� ������ Ǯ�� ��ȯ
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(555);
    }
}
