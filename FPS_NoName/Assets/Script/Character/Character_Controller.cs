using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character_Controller : MonoBehaviour
{
    [SerializeField] private Transform Player_Camera = null;

    [SerializeField] Character_Info character_Info;

    [SerializeField] [Range(0.0f, 5.0f)] private float Player_MoveSmoothTime = 0.3f;
    [SerializeField] private float Player_JumpSpeed;
    [SerializeField] private float ADS_Speed;
    [SerializeField] private float Gravity;
    [SerializeField] private float mouse_Sensitivity;
    [SerializeField] [Range(0.0f, 5.0f)] private float Mouse_MoveSmoothTime = 0.04f;

    [SerializeField] private bool LockCursor;

    Vector2 CurrentDirection = Vector2.zero;
    Vector2 CurrentDirectionVelocity = Vector2.zero;

    Vector2 CurrentMouseDeleta = Vector2.zero;
    Vector2 CurrentMouseDeletaVelocity = Vector2.zero;

    private float CameraPitch = 0.0f;
    private float VelocityY = 0.0f;
    
    CharacterController controller = null;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        CursorSystem();
        MouseSystem();
        PlayerMovement();
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
    void MouseSystem()
    {
        Vector2 TargetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        CurrentMouseDeleta = Vector2.SmoothDamp(CurrentMouseDeleta, TargetMouseDelta, ref CurrentMouseDeletaVelocity, Mouse_MoveSmoothTime);

        CameraPitch -= CurrentMouseDeleta.y * mouse_Sensitivity;

        CameraPitch = Mathf.Clamp(CameraPitch, -90.0f, 90.0f);

        Player_Camera.localEulerAngles = Vector3.right * CameraPitch;
        transform.Rotate(Vector3.up, CurrentMouseDeleta.x * mouse_Sensitivity);
    }

    void PlayerMovement()
    {
        Vector2 TargetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        TargetDirection.Normalize();

        CurrentDirection = Vector2.SmoothDamp(CurrentDirection, TargetDirection, ref CurrentDirectionVelocity, Player_MoveSmoothTime);

        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                VelocityY = Player_JumpSpeed;
            }
            else
            {
                VelocityY = 0.0f;
            }
        }
        else
        {
            VelocityY += Gravity * Time.deltaTime;
        }

        Vector3 velocity = (transform.forward * CurrentDirection.y + transform.right * CurrentDirection.x) * character_Info.Character_MovementSpeed + Vector3.up * VelocityY;

        controller.Move(velocity * Time.deltaTime);
    }
}
