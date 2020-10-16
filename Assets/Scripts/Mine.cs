using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    
    public GameObject mine;
    // Start is called before the first frame update
    void Start()
    {
            mine = GameObject.Find("BoomMine");
        
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
           
            Vector3 hitdirection = collision.transform.position - transform.position;
               hitdirection = hitdirection.normalized;

            FindObjectOfType<PlayerStatManager>().HurtPlayer(hitdirection);
            Destroy(mine);
        }


    }
}
