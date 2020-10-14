using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;
    public float targetHeight;

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        {
        offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //get X pos of mouse and rotate the target.
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        //get the Y pos of the mouse and rotate the pivot.
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        //move the cam based on target current rotation and the offset
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        //Limit the up/down camera rotation

        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

            Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

        transform.position = target.position; //- (rotation * offset);
        transform.rotation = Quaternion.Euler(pivot.eulerAngles.x, target.eulerAngles.y, pivot.eulerAngles.z);

        //Move camera by offset away from target
        transform.Translate(offset, Space.Self);

        //Set camera height to not go below the targetheight threshold
        if(transform.position.y < target.position.y - targetHeight)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - targetHeight, transform.position.z);
        }
    }
}
