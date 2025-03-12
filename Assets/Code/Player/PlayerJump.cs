using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJump : MonoBehaviour
{
    [SerializeField]private bool isGround = true;
    public bool isground => isGround;

    private bool isFalling = false;
    private float acceleration = 13;

    private Animator animator;
    private int animHash_idleJump = Animator.StringToHash("isJump");// ���߿� �ִϸ��̼� Ʈ���� ��������
    private int animHash_isGround = Animator.StringToHash("isGround");// �����ϴ� ���� �����ϱ� ����

    private CharacterController controller;
    private PlayerStat playerStat;
    private PlayerAniEvent playerAniEvent;

    private void Update()
    {

        //GroundCheck();
    }

    private void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        animator = GetComponentInChildren<Animator>();//ĳ���� ��Ʈ�ѷ� ��ġ�� ����
        playerAniEvent = GetComponentInChildren<PlayerAniEvent>();

        TryGetComponent<PlayerStat>(out playerStat);
        //TryGetComponent<Animator>(out animator);
        StartCoroutine("FallJump");
        
    }

    List<AnimationTag> attacktypes = new List<AnimationTag> { AnimationTag.Attack1, AnimationTag.Attack2, AnimationTag.Attack3, AnimationTag.Attack4, AnimationTag.Attack5,
                                                                AnimationTag.Attack6, AnimationTag.Attack7,AnimationTag.Attack8};
    List<AnimationTag> Damagetypes = new List<AnimationTag> { AnimationTag.sDamage, AnimationTag.mDamage, AnimationTag.Air, AnimationTag.Down, AnimationTag.Stand };


    IEnumerator FallJump()
    {

        while (true)
        {
            if (isGround)
            {
                //�������� �ƴҶ�
                if (Input.GetButtonUp("Fire3") && isGround && !(attacktypes.Contains(playerStat.aniState)) && !(Damagetypes.Contains(playerStat.aniState)))//
                {
                    isGround = false;
                    
                    yield return StartCoroutine(Jump());
                }

            }
            else
            {
                if (!isFalling)
                {
                    yield return StartCoroutine("Falling");
                    
                }
             
            }
            yield return null;
        }
    }

    IEnumerator Jump()
    {
        isGround = false; // ���� �Ϸ�
        //Debug.Log("����");
        animator.ResetTrigger("isAttack1");
        animator.ResetTrigger("isSAttack1");

        animator.SetBool("isGround2", false);


        animator.ResetTrigger("isFAttack1");// ���� ����Ű ��Ÿ�ϰ� �����ϰ� �ٽ� �����ϸ�, ������ ������ ���� ����
        animator.SetTrigger(animHash_idleJump);
        EventManager.Instance.TriggerEvent();// ���� ���� �� ���� üũ
        

        Vector3 startPos = transform.position;
        Vector3 jumpPos = transform.position + new Vector3(0, 1, 0);

        float duration = 0.5f; // �����ϴ� �ð� (0.5��)
        float elapsed = 0f; // ��� �ð�

        while (elapsed < duration)
        {

            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(startPos.y, jumpPos.y, 1-(1- elapsed / duration)*(1- elapsed / duration)),//EaseOut
                transform.position.z);


            elapsed += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }

        
        transform.position = new Vector3(transform.position.x, jumpPos.y, transform.position.z);

        //isGround = false; // ���� �Ϸ�

        yield return null; // ���� �����ӱ��� ���
    }

    IEnumerator Falling()
    {
        isFalling = true;
        //Debug.Log("1���� ������");
        //yield return new WaitForSeconds(0.8f);

        float timer = 0;
        float currentFallSpeed = 0f;
        while (!isGround) // �ٴڿ� ���� ������ �ݺ�
        {
            
            timer += Time.deltaTime;
            currentFallSpeed = Mathf.Min(1 + acceleration * timer, 10);//(timer - 0.2f)
            //transform.position += Vector3.down * currentFallSpeed * Time.deltaTime;//�ڽ� ����

            controller.Move(Vector3.down * currentFallSpeed * Time.deltaTime);//ĳ���� ��Ʈ�ѷ� ����
            

            GroundCheck();
            yield return null;
        }

        isFalling = false;
    }
    
    public void GroundCheck()
    {
        
        // ���� ���� ��� ���� ��, �ְ�ġ ������ �κ� ���� Ÿ���� �����Ͽ� ���� ������ �������� ���� �� ���� Ŭ ��� ���� ������ �߰�
        if (transform.position.y <= 0)
        {
            //Debug.Log(99);
            isGround = true;
            //animator.SetTrigger(animHash_isGround);
            animator.SetBool("isGround2", true);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            playerAniEvent.isGroundjumpAttackCoi();


            //EventManager.Instance.TriggerEvent();// ���� ����� �� �ִϸ��̼� üũ
        }
    }

}
