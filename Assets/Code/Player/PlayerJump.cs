using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJump : MonoBehaviour
{
    [SerializeField]private bool isGround = true;
    private bool isFalling = false;
    private float acceleration = 13;

    private Animator animator;
    private int animHash_idleJump = Animator.StringToHash("isJump");// 나중에 애니메이션 트리로 만들어야함
    private int animHash_isGround = Animator.StringToHash("isGround");


    private void Update()
    {

        //GroundCheck();
    }

    private void Start()
    {
        TryGetComponent<Animator>(out animator);
        StartCoroutine("FallJump");
        
    }




    IEnumerator FallJump()
    {

        while (true)
        {
            if (isGround)
            {

                if (Input.GetKeyUp(KeyCode.Space)&& isGround)
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

        Debug.Log("점프");
        animator.SetTrigger(animHash_idleJump);

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

        isGround = false; // 점프 완료

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
            //transform.position += new Vector3(0, Physics.gravity.y*Time.deltaTime, 0);

            timer += Time.deltaTime;
            currentFallSpeed = Mathf.Min(1 + acceleration * timer, 10);//(timer - 0.2f)
            transform.position += Vector3.down * currentFallSpeed * Time.deltaTime;
            

            GroundCheck();
            yield return null;
        }

        isFalling = false;
    }
    
    void GroundCheck()
    {
        //Debug.Log(1);
        // 추후 높게 띄어 졌을 때, 최고치 높이인 부분 부터 타임을 측정하여 땅에 도달할 때까지를 비교해 그 값이 클 경우 낙하 데미지 추가
        if (transform.position.y <= 0)
        {
            
            isGround = true;
            animator.SetTrigger(animHash_isGround);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

}
