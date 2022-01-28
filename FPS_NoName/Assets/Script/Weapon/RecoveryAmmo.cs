using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryAmmo : MonoBehaviour
{
    [SerializeField] private int RecoveryValue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Character_State>().RecovAmmo(RecoveryValue);
        }
    }
}
