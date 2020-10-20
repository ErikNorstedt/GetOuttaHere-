using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float maxSpeed;
    public float maxAcceleration;
    public float jumpForce;

    bool isGrounded;
    float jumpRememberPressed;
    float jumpRememberPressedTime;

    public Vector3 desiredVelocity;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Horizontal movement derived from a tutorial at catlikecoding.com
        desiredVelocity = transform.TransformDirection(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")) * maxSpeed;

        velocity = rb.velocity;

        float maxSpeedDelta = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedDelta);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedDelta);


        jumpRememberPressed -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpRememberPressed = jumpRememberPressedTime;
        }

        if (isGrounded && jumpRememberPressed > 0f) //TODO: determine isGrounded
        {
            jumpRememberPressed = 0f;
            velocity.y = jumpForce;
        }

        rb.velocity = velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
