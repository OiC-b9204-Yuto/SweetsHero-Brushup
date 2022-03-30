using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAct : MonoBehaviour
{


    //����𑝂₷�Ȃ烊�X�g��
    [SerializeField] GameObject weapon_object;
    [SerializeField] WeaponInput weaponInput;

    [SerializeField] Character_Info character_Info;

    //���퓙
    [SerializeField] private Animator weapon_animator;

    //�ړ��֘A(�L�[�{�[�h����)
    [SerializeField] [Range(0.0f, 5.0f)] private float moveSmoothTime = 0.3f;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float Gravity;

    private float VelocityY = 0.0f;

    Vector2 CurrentDirection = Vector2.zero;
    Vector2 CurrentDirectionVelocity = Vector2.zero;

    CharacterController controller = null;

    //���_�֘A(�}�E�X����)
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

        //�ړ��X�V <- �v�֐���?
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

    //Normalize���ǂ����ł�邩�H <- ���g���ړ��ʂ̎󂯎��ɂȂ������疽������l����������
    //���������}�E�X���ړ��͈ړ������疼�O�l��������
    public bool movement(Vector2 direction)
    {
        direction.Normalize();

        character_Info.Character_IsMove = (direction != new Vector2(0, 0));

        CurrentDirection = Vector2.SmoothDamp(CurrentDirection, direction, ref CurrentDirectionVelocity, moveSmoothTime);
        return true;
    }
    //���������L�Ɠ��l
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

        //�J�����̌X���̌��x -90���`90��
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
