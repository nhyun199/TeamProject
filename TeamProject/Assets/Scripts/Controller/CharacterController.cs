using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody _rigidbody;
    private float xDirection;
    private float yDirection;
    public float moveSpeed;
    public Vector3 moveDirection;

    public Transform orientation;

    public float groundDrag;
    public LayerMask IsGround;
    public float playerHeight;
    bool grounded;

    public float jumpForce;
    public bool readyToJump;
    public float jumpCoolDown;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.5f + 2f, IsGround);
        MyInput();
       if(grounded)
        {
            _rigidbody.drag = groundDrag;
        }
       else
        {
            _rigidbody.drag = 0;
        }
        SpeedControl();
        if(Input.GetKey(KeyCode.Space) && grounded && readyToJump)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
        
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    public void MyInput()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        yDirection = Input.GetAxisRaw("Vertical");
    }
    public void MovePlayer()
    {
        moveDirection = orientation.forward * yDirection + orientation.right * xDirection;
        _rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    public void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
    }
    public void Jump()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(transform.position * jumpForce, ForceMode.Impulse);
    }
    public void ResetJump()
    {
        readyToJump = true;
    }
}
