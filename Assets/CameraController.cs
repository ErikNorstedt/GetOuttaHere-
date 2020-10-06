using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform Pivot;

    public float maxViewAngle;
    public float minViewAngle;

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        {
        offset = target.position - transform.position;
        }

        Pivot.transform.position = target.transform.position;
        Pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //get X pos of mouse and rotate the target.
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        //get the Y pos of the mouse and rotatethe pivot.
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        Pivot.Rotate(-vertical, 0, 0);

        //move the cam based on target current rotation and the offset
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = Pivot.eulerAngles.x;

        //Limit the up/down camera rotation

        if (Pivot.rotation.eulerAngles.x > maxViewAngle && Pivot.rotation.eulerAngles.x < 180f)
        {
            Pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if (Pivot.rotation.eulerAngles.x > 180f && Pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            Pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

            Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

        transform.position = target.position; //- (rotation * offset);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, target.rotation.y, transform.eulerAngles.z);
        //transform.position = target.position - offset;

        transform.Translate(offset, Space.Self);

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }

        //transform.LookAt(target);
    }
}
