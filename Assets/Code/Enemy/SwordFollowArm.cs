using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFollowArm : MonoBehaviour
{
    [SerializeField] private Transform Hand;
    // Update is called once per frame
    void FixedUpdate()
    {
        FollowSword();
    }
       

    void FollowSword()
    {
        gameObject.transform.position = Hand.position;
        gameObject.transform.rotation = Hand.rotation;
    }
}
