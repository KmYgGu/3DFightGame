using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySwordWind : MonoBehaviour
{
    public float maxSpeed = 20f;  // 최대 속도
    public float acceleration = 200f;  // 가속도
    private float speed;// = 0f;  // 현재 속도
    private ObjectPool<EnemySwordWind> pool;

    private BoxCollider box;

    //[SerializeField] PlayerHitBox hitBox;
    public delegate void PlayerSWDamaged(EnemySwordWind SWD);
    public static event PlayerSWDamaged PlayerSWDam;

    public void Setpool(ObjectPool<EnemySwordWind> WindPool)
    {
        pool = WindPool;
    }

    private void OnEnable()
    {
        speed = 0f;
        box.enabled = true;
        // 일정 시간 후 자동 반환
        Invoke(nameof(ReturnToPool), 2f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Awake()
    {
        TryGetComponent<BoxCollider>(out box);
    }

    private void Update()
    {
        Invoke(nameof(Move), 0.5f);// 학원 컴퓨터에선 빠름


    }

    void Move()
    {
        //transform.Translate(Vector3.forward * 3 * Time.deltaTime);
        speed += acceleration * Time.deltaTime; // 가속도 적용 (점점 빨라짐)
        speed = Mathf.Min(speed, maxSpeed); // 최대 속도 제한
        transform.Translate(Vector3.forward * speed * Time.deltaTime);// 학원 컴퓨터에선 빠름
    }

    public void ReturnToPool()
    {
        pool.Release(this); // 원거리 공격을 풀로 반환
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            //Debug.Log("플레이어에게 부딪힘!");
            box.enabled = false;

            PlayerSWDam?.Invoke(this);
        }

    }
}
