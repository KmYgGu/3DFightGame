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

        Debug.Log("����");

        Vector3 startPos = transform.position;
        Vector3 jumpPos = transform.position + new Vector3(0, 1, 0);

        float duration = 0.5f; // �����ϴ� �ð� (0.5��)
        float elapsed = 0f; // ��� �ð�

        while (elapsed < duration)
        {
            //transform.position = Vector3.Lerp(startPos, jumpPos, elapsed / duration);

            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(startPos.y, jumpPos.y, elapsed / duration),
                transform.position.z);


            elapsed += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }

        //transform.position = jumpPos; // ��Ȯ�� ��ġ ����
        transform.position = new Vector3(transform.position.x, jumpPos.y, transform.position.z);

        isGround = false; // ���� �Ϸ�


    }

    IEnumerator Falling()
    {
        isFalling = true;
        Debug.Log("1���� ������");
        yield return new WaitForSeconds(0.8f);

        while (!isGround) // �ٴڿ� ���� ������ �ݺ�
        {
            transform.position += new Vector3(0, Physics.gravity.y/900, 0);
            yield return null;
        }

        isFalling = false;
    }
}
