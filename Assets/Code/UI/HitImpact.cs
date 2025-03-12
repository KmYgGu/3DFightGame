using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class HitImpact : MonoBehaviour
{
    private ObjectPool<HitImpact> pool;

    private ParticleSystem ps;

    private void Awake()
    {
        TryGetComponent<ParticleSystem>(out ps);
    }

    public void Setpool(ObjectPool<HitImpact> WindPool)
    {
        pool = WindPool;
    }

    private void OnEnable()
    {
        ps.Play();
        // ���� �ð� �� �ڵ� ��ȯ
        Invoke(nameof(ReturnToPool), 2f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void ReturnToPool()
    {
        pool.Release(this); // ���Ÿ� ������ Ǯ�� ��ȯ
    }
}
