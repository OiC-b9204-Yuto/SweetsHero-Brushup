using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryAmmo : MonoBehaviour
{
    [SerializeField] private int RecoveryValue;
    [SerializeField] private AudioClip SoundRecovAmmo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AudioManager.Instance.SE.PlayOneShot(SoundRecovAmmo);
            other.GetComponent<Character_State>().RecovAmmo(RecoveryValue);
        }
    }
}
