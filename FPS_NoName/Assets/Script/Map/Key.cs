using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private AudioClip SoundGetKey;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AudioManager.Instance.SE.PlayOneShot(SoundGetKey);
            other.GetComponent<Character_Info>().Character_GetKeys++;
            Destroy(this.gameObject);        }
    }
}
