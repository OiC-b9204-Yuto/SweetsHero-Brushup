using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    UI_MainGame MainGameUI_System;
    [SerializeField] private CharacterAct characterAct;
    [SerializeField] private GameObject MainGameUI;
    [SerializeField] private Transform Player_Camera = null;
    [SerializeField] private bool LockCursor;



    // Start is called before the first frame update
    void Awake()
    {
        MainGameUI_System = MainGameUI.GetComponent<UI_MainGame>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (MainGameUI_System.isStartAnimation) return;
        if (MainGameUI_System.isGameClear || MainGameUI_System.isGameOver) return;

        CursorSystem();

        characterAct.viewpointMove(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        characterAct.movement(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

        if (Input.GetKey(KeyCode.Space))
        {
            characterAct.jump();
        }

        if(Input.GetKeyDown(Custom_InputManager.Instance.inputData.Chara_Reload))
        {
            characterAct.weaponReload();
        }

        if(Input.GetKeyDown(Custom_InputManager.Instance.inputData.Chara_Fire))
        {
            characterAct.weaponShoot();
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            characterAct.throwGrenade();
        }
        
    }

    void CursorSystem()
    {
        if (LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
