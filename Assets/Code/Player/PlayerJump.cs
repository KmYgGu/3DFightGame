using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJump : MonoBehaviour
{
    [SerializeField]private bool isGround = true;
    public bool isground => isGround;

    //private bool isFalling = false;
    public bool isFalling;// = false;
    private float acceleration = 13;

    private Animator animator;
    private int animHash_idleJump = Animator.StringToHash("isJump");// 나중에 애니메이션 트리로 만들어야함
    private int animHash_isGround = Animator.StringToHash("isGround");// 점프하는 상태 해제하기 위해

    private CharacterController controller;
    private PlayerStat playerStat;
    private PlayerAniEvent playerAniEvent;

    private void Awake()
    {
        TryGetComponent<CharacterController>(out controller);
        animator = GetComponentInChildren<Animator>();//캐릭터 컨트롤러 위치에 놓음
        playerAniEvent = GetComponentInChildren<PlayerAniEvent>();

        TryGetComponent<PlayerStat>(out playerStat);
    }

    private void OnEnable()
    {
        
        EventManager.Instance.PlayerDied += StopMove;
        EventManager.Instance.EnemyDied += StopMove;
    }

    private void OnDisable()
    {
        
        EventManager.Instance.PlayerDied -= StopMove;
        EventManager.Instance.EnemyDied -= StopMove;
    }
    private void Start()
    {
                
        StartCoroutine("FallJump");
        
    }
    

    List<AnimationTag> attacktypes = new List<AnimationTag> { AnimationTag.Attack1, AnimationTag.Attack2, AnimationTag.Attack3, AnimationTag.Attack4, AnimationTag.Attack5,
                                                                AnimationTag.Attack6, AnimationTag.Attack7,AnimationTag.Attack8};
    List<AnimationTag> Damagetypes = new List<AnimationTag> { AnimationTag.sDamage, AnimationTag.mDamage, AnimationTag.Air, AnimationTag.Down, AnimationTag.Stand };

    void FallJumpUpdate()
    {
        if (isGround)
        {
            //공격중이 아닐때
            if (Input.GetButtonUp("Fire3") && isGround && !(attacktypes.Contains(playerStat.aniState)) && !(Damagetypes.Contains(playerStat.aniState)))//
            {
                isGround = false;

                StartCoroutine(Jump());
            }

        }
        else
        {
            if (!isFalling)
            {
                StartCoroutine("Falling");

            }

        }
    }
    public IEnumerator FallJump()
    {

        while (true)
        {
            if (isGround)
            {
                //공격중이 아닐때
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
        isGround = false; // 점프 완료
        //Debug.Log("점프");
        animator.ResetTrigger("isAttack1");
        animator.ResetTrigger("isSAttack1");

        animator.SetBool("isGround2", false);


        animator.ResetTrigger("isFAttack1");// 점프 공격키 연타하고 착지하고 다시 점프하면, 공격이 나가는 것을 방지
        animator.SetTrigger(animHash_idleJump);
        EventManager.Instance.TriggerEvent();// 점프 했을 때 상태 체크
        

        Vector3 startPos = transform.position;
        Vector3 jumpPos = transform.position + new Vector3(0, 1, 0);

        float duration = 0.5f; // 점프하는 시간 (0.5초)
        float elapsed = 0f; // 경과 시간

        while (elapsed < duration)
        {

            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(startPos.y, jumpPos.y, 1-(1- elapsed / duration)*(1- elapsed / duration)),//EaseOut
                transform.position.z);


            elapsed += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        
        transform.position = new Vector3(transform.position.x, jumpPos.y, transform.position.z);

        //isGround = false; // 점프 완료

        yield return null; // 다음 프레임까지 대기
    }

    IEnumerator Falling()
    {
        isFalling = true;
        //Debug.Log("1초후 떨어짐");
        //yield return new WaitForSeconds(0.8f);

        float timer = 0;
        float currentFallSpeed = 0f;
        while (!isGround) // 바닥에 닿을 때까지 반복
        {
            
            timer += Time.deltaTime;
            currentFallSpeed = Mathf.Min(1 + acceleration * timer, 10);//(timer - 0.2f)
            //transform.position += Vector3.down * currentFallSpeed * Time.deltaTime;//자식 전용

            controller.Move(Vector3.down * currentFallSpeed * Time.deltaTime);//캐릭터 컨트롤러 전용
            

            GroundCheck();
            yield return null;
        }

        isFalling = false;
    }
    
    public void GroundCheck()
    {
        
        // 추후 높게 띄어 졌을 때, 최고치 높이인 부분 부터 타임을 측정하여 땅에 도달할 때까지를 비교해 그 값이 클 경우 낙하 데미지 추가
        if (transform.position.y <= 0)
        {
            //Debug.Log(99);
            isGround = true;
            //animator.SetTrigger(animHash_isGround);
            animator.SetBool("isGround2", true);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            playerAniEvent.isGroundjumpAttackCoi();


            //EventManager.Instance.TriggerEvent();// 땅에 닿았을 때 애니메이션 체크
        }
    }

    void StopMove()
    {
        //StopAllCoroutines();
    }

}
