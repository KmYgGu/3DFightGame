using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollDamage : MonoBehaviour
{
    [SerializeField] private int Damage;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ground")) return;

        if (other.CompareTag("PlayerBody")) return;

        //if(other.TryGetComponent<>)
    }
}
