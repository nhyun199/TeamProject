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
        yRotation += mouseX;  // X������ ī�޶� �¿� ������. ī�޶�� y���� �¿��̴�.

        xRotation -= mouseY;  // Y�� ���� ī�޶� ���Ʒ������� . ī�޶�� x���� ���Ʒ�.
        xRotation = Mathf.Clamp(xRotation,-90f,90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0); // �ٶ󺸴¹��⸸ �¿츸�Ǵ�.
    }
}
