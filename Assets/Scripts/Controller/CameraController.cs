using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private float mouseX;
    private float mouseY;
    public float sensX = -1f;
    public float sensY = -1f;

    private float xRotation;
    private float yRotation;

    public Transform orientation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;  // X로인해 카메라 좌우 움직임. 카메라는 y쪽이 좌우이다.

        xRotation -= mouseY;  // Y로 인해 카메라 위아래움직임 . 카메라는 x쪽이 위아래.
        xRotation = Mathf.Clamp(xRotation,-90f,90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0); // 바라보는방향만 좌우만판단.
    }
}
