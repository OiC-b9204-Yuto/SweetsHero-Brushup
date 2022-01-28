using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomKeySet : MonoBehaviour
{
    [SerializeField] private Transform[] SpawnPoint;
    [SerializeField] private GameObject[] SpawnObject;
    [SerializeField] private int HitValue;
    private int RandomValue;
    // Start is called before the first frame update
    void Start()
    {
        RandomValue = SpawnPoint.Length;

        int hitcount = 0;

        for(int i = 0; i < RandomValue; i++)
        {
            if(hitcount != HitValue)
            {
                if (RandomValue - i <= HitValue - hitcount)
                {
                    hitcount++;
                    Instantiate(SpawnObject[0], SpawnPoint[i]);
                    continue;
                }
                int j = Random.Range(0, 2);
                if ( j == 0)
                {
                    

                    hitcount++;
                    Instantiate(SpawnObject[0], SpawnPoint[i]);
                    continue;
                }
            }
            Instantiate(SpawnObject[1], SpawnPoint[i]);
        }
    }
}
