using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_State : MonoBehaviour
{
    //•¡”•Ší‚Ö‚Ì‘Î‰ž‚Ì‚½‚ßƒŠƒXƒg‰»‚·‚é—\’è
    [SerializeField] Weapon_State weapon_state;
    [SerializeField] Character_Info character_Info;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon_state.Reload();
        }

        if (Input.GetButton("Fire1"))
        {
            weapon_state.Shot();
        }
        float wheelroll = Input.GetAxis("Mouse ScrollWheel");
        if(wheelroll > 0)
        {
            character_Info.Character_CurrentWeapon--;
        }
        else if(wheelroll < 0)
        {
            character_Info.Character_CurrentWeapon++;
        }
    }
}
