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
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            ESWPool = new ObjectPool<HitImpact>(
                createFunc: () => Instantiate(ESWPrefab), // ������Ʈ�� ������ �� �����ϴ� ���
                actionOnGet: ESW => ESW.gameObject.SetActive(true), // Ǯ���� ������ �� ������ �۾�
            actionOnRelease: ESW => ESW.gameObject.SetActive(false),// ��ȯ �ɶ� ������ �۾�
            actionOnDestroy: ESW => Destroy(ESW.gameObject),// Ǯ���� ���� �� ��
            collectionCheck: false,                         // true �� Ǯ���� ��ü�� �ߺ� ��ȯ�Ǵ� �� �˻�
            defaultCapacity: 5,                            // �⺻���� ������ ��ü�� 
            maxSize: 10                                     // �ִ� ��ü�� �ʰ��ϸ� Ǯ������ �ʰ� ���� 
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
