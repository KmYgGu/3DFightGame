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
        // 일정 시간 후 자동 반환
        Invoke(nameof(ReturnToPool), 2f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void ReturnToPool()
    {
        pool.Release(this); // 원거리 공격을 풀로 반환
    }
}
