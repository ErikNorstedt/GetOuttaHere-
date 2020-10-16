using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour
{
    public GameObject bullet;
    public Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0,0);
    }
    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = new Vector3(0f,0f,0f);
        speed = 0f;
        //if (other.collider.tag == "Player")
        //{
        bullet.transform.SetParent(other.transform);
        bullet.transform.parent = other.transform;
            bullet.GetComponent<Rigidbody>().useGravity = false;
            bullet.GetComponent<SphereCollider>().isTrigger = true;
        //}
        
    }
    private void OnTriggerEnter(Collider other)
    {
    }
}
