using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAct : MonoBehaviour
{


    //武器を増やすならリストに
    [SerializeField] GameObject weapon_object;
    [SerializeField] WeaponInput weaponInput;

    [SerializeField] Character_Info character_Info;

    //武器等
    [SerializeField] private Animator weapon_animator;

    //移動関連(キーボード操作)
    [SerializeField] [Range(0.0f, 5.0f)] private float moveSmoothTime = 0.3f;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float Gravity;

    private float VelocityY = 0.0f;

    Vector2 CurrentDirection = Vector2.zero;
    Vector2 CurrentDirectionVelocity = Vector2.zero;

    CharacterController controller = null;

    //視点関連(マウス操作)
    [SerializeField] private Transform camera = null;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] [Range(0.0f, 5.0f)] private float mouseSmoothTime = 0.04f;


    Vector2 CurrentMouseDelta = Vector2.zero;
    Vector2 CurrentMouseDeltaVelocity = Vector2.zero;

    private float CameraPitch = 0.0f;

    [SerializeField] [Range(0.0f, 180.0f)] private float CameraMaxPitch = 90;   
    [SerializeField] [Range(-180.0f, 0.0f)] private float CameraMinPitch = -90;


    private void Awake()
    {
        GameData_Manager.Instance.Load();
        mouseSensitivity = GameData_Manager.Instance.gameData.MouseSensitivity;
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {

        //移動更新 <- 要関数化?
        if(!controller.isGrounded)
        {
            VelocityY += Gravity * Time.deltaTime;
        }
        else if(VelocityY <= 0)
        {
            VelocityY = 0;
        }
        Vector3 velocity = (transform.forward * CurrentDirection.y + transform.right * CurrentDirection.x) * character_Info.Character_MovementSpeed + Vector3.up * VelocityY;
        controller.Move(velocity * Time.deltaTime);

    }

    //Normalizeをどっちでやるか？ <- 中身が移動量の受け取りになったから命名から考え直しかも
    //そもそもマウスも移動は移動だから名前考え直そう
    public bool movement(Vector2 direction)
    {
        direction.Normalize();

        character_Info.Character_IsMove = (direction != new Vector2(0, 0));

        CurrentDirection = Vector2.SmoothDamp(CurrentDirection, direction, ref CurrentDirectionVelocity, moveSmoothTime);
        return true;
    }
    //こちらも上記と同様
    public bool jump()
    {
        if (!controller.isGrounded) return false;
        VelocityY = jumpSpeed;
        return true;
    }

    public bool viewpointMove(Vector2 delta)
    {
        CurrentMouseDelta = Vector2.SmoothDamp(CurrentMouseDelta, delta, ref CurrentMouseDeltaVelocity, mouseSmoothTime);

        CameraPitch -= CurrentMouseDelta.y * mouseSensitivity;

        //カメラの傾きの限度 -90°～90°
        CameraPitch = Mathf.Clamp(CameraPitch, CameraMinPitch, CameraMaxPitch);

        camera.localEulerAngles = Vector3.right * CameraPitch;
        transform.Rotate(Vector3.up, CurrentMouseDelta.x * mouseSensitivity);
        return true;
    }


    public bool weaponShot()
    {
        if (character_Info.Character_IsReload) return false;

        if (weaponInput.Shot())
        {     
            weapon_animator.SetBool("IsShot", true);
            return true;
        }

        weapon_animator.SetBool("IsShot", false);
        return false;
    }

    public bool weaponReload()
    {
        if(!weaponInput.Reload()) return false;
        weapon_animator.SetTrigger("TriggerReload");
        return true;
    }   

    public void RecovAmmo(int value)
    {
        weaponInput.RecovAmmo(value);
    }
}
