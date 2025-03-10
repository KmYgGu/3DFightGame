using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    //�ɷ�ġ�� ��ȭ�� ���� ������ EventManager�� ȣ��
    [SerializeField] private AnimationTag AniState;
    public AnimationTag aniState => AniState;// ���� ������ ���� �ʴ� �Լ�

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

    // �浹�� ������Ʈ(����)�� ������ HashSet


    private void Start()
    {
        tagReader = gameObject.GetComponentInChildren<AnimationTagReader>();

        AniState = AnimationTag.Idle; // ó������ idle������ �ʱ�ȭ

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

        yield return new WaitForEndOfFrame(); // �� ������ ��� �� ����

        AniState = tagReader.GetCurrentAnimationTag();

        tagReader.GetCurrentAnimationTag();

        //Debug.Log("�÷��̾� ���� �ִϸ��̼� �±�: " + AniState);

    }

    private void HpDamage(PlayerAttackBox EnemyDam)
    {
        //EnemyHp -= 100;
        enemyHp -= 100;
    }
}
