using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_CheckKeys : MonoBehaviour
{
    [SerializeField] private Character_Info info;
    [SerializeField] private GameObject FlontGate;
    [SerializeField] private AudioClip SoundGate;

    private bool end;

    private void Awake()
    {
        end = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (end) return;

        if (info.Character_GetKeys == 3)
        {
            AudioManager.Instance.SE.PlayOneShot(SoundGate);
            FlontGate.SetActive(false);
            end = true;
        }
    }
}
