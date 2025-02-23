using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]private bool isGround = true;
    private bool isFalling = false;

    PlayerControler playctn;


    private void Update()
    {
        //Jump();
        //Falling();
    }

    private void Start()
    {
        StartCoroutine("FallJump");
    }

    IEnumerator FallJump()
    {

        while (true)
        {
            if (isGround)
            {

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    StartCoroutine(Jump());
                    isGround = false;
                }

            }
            else
            {
                if (!isFalling)
                {
                    //yield return StartCoroutine("Falling");
                }
                

            }
            yield return null;
        }
    }

    IEnumerator Jump()
    {

        Debug.Log("점프");

        Vector3 startPos = transform.position;
        Vector3 jumpPos = transform.position + new Vector3(0, 1, 0);

        float duration = 0.5f; // 점프하는 시간 (0.5초)
        float elapsed = 0f; // 경과 시간

        while (elapsed < duration)
        {
            //transform.position = Vector3.Lerp(startPos, jumpPos, elapsed / duration);

            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(startPos.y, jumpPos.y, elapsed / duration),
                transform.position.z);


            elapsed += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        //transform.position = jumpPos; // 정확한 위치 보정
        transform.position = new Vector3(transform.position.x, jumpPos.y, transform.position.z);

        isGround = false; // 점프 완료


    }

    IEnumerator Falling()
    {
        isFalling = true;
        Debug.Log("1초후 떨어짐");
        yield return new WaitForSeconds(0.8f);

        while (!isGround) // 바닥에 닿을 때까지 반복
        {
            transform.position += new Vector3(0, Physics.gravity.y/900, 0);
            yield return null;
        }

        isFalling = false;
    }
}
