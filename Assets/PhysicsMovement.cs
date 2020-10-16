using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal"));
        
        moveDirection.y = yStore;

        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z).normalized * speed* Time.deltaTime;
        //rb.velocity = rb.velocity.normalized;

        if (Input.GetButtonDown("Jump"))
        {
            
        }

    }
}
