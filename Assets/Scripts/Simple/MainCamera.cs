using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float speedCamera;
    public float strangeScroll;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position -= new Vector3(speedCamera, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(speedCamera, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(0, 0, speedCamera) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0, 0, speedCamera) * Time.deltaTime;
        }
        transform.position -= new Vector3(0, Input.mouseScrollDelta.y, 0) * strangeScroll;
    }
}
