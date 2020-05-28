using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : UnityEngine.MonoBehaviour
{
    public float speedCamera, speedCameraOnScroll, strangeScroll, smoothnessOfTheThreshold, borderUp, borderDown;
    public Vector3 cameraVector;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) //Стрелочка вверх
        {
            float y = transform.position.y;
            transform.position += transform.forward * speedCamera * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.DownArrow)) //Стрелочка вниз
        {
            float y = transform.position.y;
            transform.position -= transform.forward * speedCamera * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

        }
        if (Input.GetKey(KeyCode.LeftArrow)) //Стрелочка влево 
        {
            transform.position -= transform.right * speedCamera * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow)) //Стрелочка вправо
        {
            transform.position += transform.right * speedCamera * Time.deltaTime;
        }
        if (transform.position.y < borderUp && borderDown > transform.position.y)
        {
            transform.position -= new Vector3(0, Input.mouseScrollDelta.y, 0) * strangeScroll;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > 272 && transform.position.y < 632)
        {
            transform.Rotate(-Time.deltaTime * smoothnessOfTheThreshold, 0, 0);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y > 272 && transform.position.y < 632)
        {
            transform.Rotate(Time.deltaTime * smoothnessOfTheThreshold, 0, 0);
        }
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
