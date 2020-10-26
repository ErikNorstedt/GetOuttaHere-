using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour
{
    public GameObject bullet;
    public Rigidbody rb;
    public float speed;

    public float blastRadius = 50f;
    public GameObject explosionEffect;
    public float blastForce = 5000f;


    public bool startCountdown;
    public float countdownToBoom = 3f;

    bool hasExploded = false;
   
    // Start is called before the first frame update
    void Start()
    {
        startCountdown = false;
        //speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

        if(startCountdown == true)
        {
            countdownToBoom -= Time.deltaTime;
        }
        if(countdownToBoom <= 0 && !hasExploded)
        {
            Debug.Log("Boom");
            Explode();
            hasExploded = true;
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        //makes it stop and starts the countdown to the boom!

        //might need to be fixed since it stops the parented object aswell...
       
        rb.velocity = new Vector3(0f, 0f, 0f);     
        speed = 0f;
        Debug.Log("Stuck");
        startCountdown = true;

            bullet.GetComponent<Rigidbody>().useGravity = false;
            //bullet.GetComponent<SphereCollider>().isTrigger = true;
            bullet.GetComponent<SphereCollider>().enabled = false;

        if (other.gameObject.tag == "Player")
        {
            //Connects the bomb to the player (could be changed to all moving things later on)
            bullet.AddComponent<FixedJoint>().connectedBody = other.rigidbody;


            /*bullet.transform.SetParent(other.transform);
            bullet.transform.parent = other.transform;*/
        }    
        
        
    }
    void Explode()
    {
        //Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(blastForce, transform.position, blastRadius);
            }
        }
        Destroy(gameObject);

    }
}
