using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject bullet;

    public float fireRate = 15f;
    private float nextTimeFire;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       

            if (Input.GetMouseButton(0) && Time.time >= nextTimeFire)
            {
                Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            nextTimeFire = Time.time + 1f / fireRate;
            }
       
    }
}
