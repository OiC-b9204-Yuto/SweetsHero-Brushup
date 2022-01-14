using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Sway : MonoBehaviour
{
    private float MouseX, MouseY;
    [SerializeField] [Range(0.0f, 30.0f)] private float SwayTime;

    void Update()
    {
        MouseInput();
        Quaternion RotationX = Quaternion.AngleAxis(-MouseY, Vector3.right);
        Quaternion RotationY = Quaternion.AngleAxis(MouseX, Vector3.up);

        Quaternion TargetRotation = RotationX * RotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, TargetRotation, SwayTime * Time.deltaTime);
    }

    void MouseInput()
    {
        MouseX = Input.GetAxisRaw("Mouse X");
        MouseY = Input.GetAxisRaw("Mouse Y");
    }
}
