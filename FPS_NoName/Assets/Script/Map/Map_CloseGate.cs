using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_CloseGate : MonoBehaviour
{
    [SerializeField] private GameObject[] Gate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Gate[0].SetActive(true);
            Gate[1].SetActive(false);
        }
    }
}
