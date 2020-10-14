using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactermovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    public float jumpRememberPressedTime;
    private float jumpRememberPressed = 0;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;


   
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        if (knockBackCounter <= 0)
        {

        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        jumpRememberPressed -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpRememberPressed = jumpRememberPressedTime;
        }

            if (controller.isGrounded)
            {
                moveDirection.y = 0f;

                if (jumpRememberPressed > 0f)
                {
                    jumpRememberPressed = 0f;
                    moveDirection.y = jumpForce;
                }
            }
        }
            else
            {
                knockBackCounter -= Time.deltaTime;
            }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
        //moveDirection = moveDirection.normalized;

    }
    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }
}
