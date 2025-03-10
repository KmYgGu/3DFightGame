using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    //능력치에 변화가 있을 때마다 EventManager를 호출
    [SerializeField] private AnimationTag AniState;
    public AnimationTag aniState => AniState;// 값을 가지고 있지 않는 함수

    [SerializeField] private float EnemyHp = 1000;

    //public float enemyHp => EnemyHp;
    public float enemyHp 
    {   get => EnemyHp;
        set
        {
            
            EnemyHp = value;

            if (EnemyHpCha != null)
            {
                
                EnemyHpCha.Invoke();
            }
                

        }

    }
    public delegate void EnemyHpChanged();
    public event EnemyHpChanged EnemyHpCha;

    private int EnemyAttackPower = 10;
    public int enemyAttackPower => EnemyAttackPower;

    private AnimationTagReader tagReader;

    [SerializeField]private bool isAttack = false;

    public bool isattack => isAttack;

    public void ChangeisAttacktrue()
    {
        isAttack = true;
    }
    public void ChangeisAttackfalse()
    {
        isAttack = false;
    }

    // 충돌한 오브젝트(공격)를 저장할 HashSet


    private void Start()
    {
        tagReader = gameObject.GetComponentInChildren<AnimationTagReader>();

        AniState = AnimationTag.Idle; // 처음에는 idle값으로 초기화

    }

    private void OnEnable()
    {

        //EventManager.Instance.OnCustomEvent3 += OnEventReceived3;
        EventManager.Instance.EnemyAni += OnEventReceived3;

        PlayerAttackBox.EnemyDam += HpDamage;
    }

    private void OnDisable()
    {

        //EventManager.Instance.OnCustomEvent3 -= OnEventReceived3;
        EventManager.Instance.EnemyAni -= OnEventReceived3;

        PlayerAttackBox.EnemyDam -= HpDamage;
    }

    private void OnEventReceived3()
    {
        StartCoroutine("GetAnimationTag");
    }

    private IEnumerator GetAnimationTag()
    {

        yield return new WaitForEndOfFrame(); // 한 프레임 대기 후 실행

        AniState = tagReader.GetCurrentAnimationTag();

        tagReader.GetCurrentAnimationTag();

        //Debug.Log("플레이어 현재 애니메이션 태그: " + AniState);

    }

    private void HpDamage(PlayerAttackBox EnemyDam)
    {
        //EnemyHp -= 100;
        enemyHp -= 100;
    }
}
