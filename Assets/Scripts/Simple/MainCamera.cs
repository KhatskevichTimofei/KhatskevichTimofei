using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : UnityEngine.MonoBehaviour
{
    public float speedCamera, speedCameraOnScroll;
    public float strangeScroll;
    public Vector3 cameraVector;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            float y = transform.position.y;
            transform.position += transform.forward * speedCamera * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float y = transform.position.y;
            transform.position -= transform.forward * speedCamera * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * speedCamera * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * speedCamera * Time.deltaTime;
        }
        transform.position -= new Vector3(0, Input.mouseScrollDelta.y, 0) * strangeScroll;

        if (Input.GetMouseButtonDown(2))
        {
            cameraVector = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 delta;
            delta = Input.mousePosition - cameraVector;
            transform.localEulerAngles += new Vector3(0, delta.x, 0) * speedCameraOnScroll;
            cameraVector = Input.mousePosition;
        }
    }
}
