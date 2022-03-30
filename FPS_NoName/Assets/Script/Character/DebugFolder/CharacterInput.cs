using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private CharacterAct characterAct;
    [SerializeField] private MainGameManager mainGameManager;
    [SerializeField] private Transform Player_Camera = null;
    [SerializeField] private bool LockCursor;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (mainGameManager.gameProgressState != GameProgressState.Game_IsGameProgress) return;

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
        else if(Input.GetKey(Custom_InputManager.Instance.inputData.Chara_Fire))
        {
            characterAct.weaponShot();
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
