using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float maxSpeed;
    public float maxAcceleration;
    [SerializeField, Range(0f, 25f)] float jumpForce = 100f;

    bool isGrounded;

    float jumpRememberPressed;
    [SerializeField, Range(0f, 1f), Tooltip("Amount of time the player can pre-emptively press jump before being on the ground")]
    float jumpRememberPressedTime = 0.2f;
    [SerializeField, Range(0f, 1f), Tooltip("Amount of upwards velocity retained when releasing the jump key")] 
    float jumpReleaseVelocityMultiplier = 0.8f;


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

        //Check jump input
        jumpRememberPressed -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpRememberPressed = jumpRememberPressedTime;
        }

        if (isGrounded && (jumpRememberPressed > 0f))
        {
            jumpRememberPressed = 0f;
            Jump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            ReleaseJump();
        }

        rb.velocity = velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Make sure that this line is the last line executed in FixedUpdate
        isGrounded = false;
    }

    void Jump()
    {
        velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpForce);
        Debug.Log("Jump!");
    }

    void ReleaseJump()
    {
        if (velocity.y > 0f)
            velocity.y *= 0.5f;
    }

    void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    void EvaluateCollision (Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            isGrounded |= normal.y >= 0.9f; //Compound: isGrounded is true if isGrounded is true OR latter statement
        }
    }
}
