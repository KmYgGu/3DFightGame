using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitImpactManager : MonoBehaviour
{
    public static HitImpactManager Instance { get; private set; }

    public HitImpact ESWPrefab;
    private ObjectPool<HitImpact> ESWPool;

    //[SerializeField] Transform EnemyParent;
    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            ESWPool = new ObjectPool<HitImpact>(
                createFunc: () => Instantiate(ESWPrefab), // 오브젝트가 부족할 때 생성하는 방법
                actionOnGet: ESW => ESW.gameObject.SetActive(true), // 풀에서 가져올 때 실행할 작업
            actionOnRelease: ESW => ESW.gameObject.SetActive(false),// 반환 될때 실행할 작업
            actionOnDestroy: ESW => Destroy(ESW.gameObject),// 풀에서 제거 될 때
            collectionCheck: false,                         // true 시 풀링된 개체가 중복 반환되는 지 검사
            defaultCapacity: 5,                            // 기본으로 생성할 개체수 
            maxSize: 10                                     // 최대 개체수 초과하면 풀링하지 않고 삭제 
                );
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnAttack(Transform transform)
    {
        HitImpact ESW = ESWPool.Get();
        //ESW.transform.SetParent(EnemyParent);
        ESW.transform.position = transform.position;
        //ESW.transform.position = new Vector3(transform.position.x+2, transform.position.y, transform.position.z);
        ESW.transform.rotation = transform.rotation;
        ESW.Setpool(ESWPool);
    }
}
