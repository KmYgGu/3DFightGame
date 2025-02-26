using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJump : MonoBehaviour
{
    [SerializeField]private bool isGround = true;
    private bool isFalling = false;
    private float acceleration = 13;

    private Animator animator;
    private int animHash_idleJump = Animator.StringToHash("isJump");// ���߿� �ִϸ��̼� Ʈ���� ��������
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

        Debug.Log("����");
        animator.SetTrigger(animHash_idleJump);

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

        isGround = false; // ���� �Ϸ�

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
        // ���� ���� ��� ���� ��, �ְ�ġ ������ �κ� ���� Ÿ���� �����Ͽ� ���� ������ �������� ���� �� ���� Ŭ ��� ���� ������ �߰�
        if (transform.position.y <= 0)
        {
            
            isGround = true;
            animator.SetTrigger(animHash_isGround);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

}
