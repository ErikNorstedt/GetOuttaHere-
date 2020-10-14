using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmFollowCam : MonoBehaviour
{
    public Transform pivot;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(0, desiredXAngle, 0);
        transform.rotation = Quaternion.Euler(pivot.eulerAngles.x, target.eulerAngles.y, 0);
    }
}
