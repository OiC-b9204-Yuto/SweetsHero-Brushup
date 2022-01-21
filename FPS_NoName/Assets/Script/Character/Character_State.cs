using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_State : MonoBehaviour
{
    [SerializeField]Weapon_State weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
           weapon.Reload();
        }

        if (Input.GetButton("Fire1"))
        {
           weapon.Shot();
        }
    }
}
