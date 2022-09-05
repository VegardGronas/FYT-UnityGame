using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewMapCameraMovement : MonoBehaviour
{
    private InputGame input;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotSpeed;
    private float rotX;
    private float rotY;

    public float rotAngle;

    private void Start()
    {
        InputSetup[] all = FindObjectsOfType<InputSetup>();
        foreach (InputSetup inp in all)
        {
            if (inp.PlayerIndex == 0)
            {
                input = inp.GetComponent<InputGame>();
            }
        }
    }

    //https://docs.unity3d.com/ScriptReference/Quaternion.Slerp.html Quaterion slerp unity documentation
    private void Update()
    {
        if (input.cameraMoveDirection.x > 0 || input.cameraMoveDirection.x < 0)
        {
            transform.position += input.cameraMoveDirection.x * moveSpeed * Time.deltaTime * transform.right;
        }
        if (input.cameraMoveDirection.y > 0 || input.cameraMoveDirection.y < 0)
        {
            transform.position += input.cameraMoveDirection.y * moveSpeed * Time.deltaTime * transform.forward;
        }

        if (input.CameraRotation != Vector2.zero)
        {
            rotX += input.CameraRotation.y * rotAngle;
            rotY -= input.CameraRotation.x * rotAngle;
            Quaternion current = transform.localRotation;
            Quaternion rotation = Quaternion.Euler(transform.rotation.x - rotX, transform.rotation.y - rotY, 0);
            transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * rotSpeed);
        }
    }
}
