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
        //speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = new Vector3(0f,0f,0f);
        speed = 0f;

            bullet.GetComponent<Rigidbody>().useGravity = false;
            bullet.GetComponent<SphereCollider>().isTrigger = true;

        if (other.gameObject.name == "Player")
        { 
        bullet.transform.SetParent(other.transform);
        bullet.transform.parent = other.transform;
        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
    }
}
