using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_CloseGate : MonoBehaviour
{
    [SerializeField] private GameObject[] Gate;
    [SerializeField] private AudioClip SoundGate;
    private bool end;

    private void Start()
    {
        end = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (end) return;
        if (other.gameObject.tag == "Player")
        {
            AudioManager.Instance.SE.PlayOneShot(SoundGate);
            Gate[0].SetActive(true);
            Gate[1].SetActive(false);
            end = true;
        }
    }
}
