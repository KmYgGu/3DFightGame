using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private bool isGround = true;
    private void Update()
    {
        Jump();
        Falling();
    }

    void Jump()
    {
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("มกวม");
                transform.position += new Vector3(0, 2, 0);//*Time.deltaTime
            }
            isGround = false;
        }
        
    }

    void Falling()
    {
        if (!isGround)
        {
             
            transform.position += new Vector3(0, Physics.gravity.y, 0);
        }
    }
}
