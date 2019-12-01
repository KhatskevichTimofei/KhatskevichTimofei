using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Vector3 startMouse;
    public Vector3 stopMouse;
    public Image image1;
    public List<Unit> allUnits = new List<Unit>();
    public List<Unit> selectedUnits = new List<Unit>();
    public Button continueButton;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            selectedUnits.Clear();
            selectedUnits.Add(allUnits[Random.Range(0, allUnits.Count)]);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit casthit;

            if (Physics.Raycast(ray, out casthit))
            {
                for (int i = 0; i < selectedUnits.Count; i++)
                {
                    selectedUnits[i].SetTargetPosition(casthit.point);
                }
            }
            //    if (agent.CalculatePath(casthit.point, path))
            //    {
            //        agent.SetPath(path);
            //    }

        }
        if (Input.GetMouseButtonDown(1))
        {
            startMouse = Input.mousePosition;
            selectedUnits.Clear();
            image1.enabled = true;

        }
        if (Input.GetMouseButton(1))
        {
            stopMouse = Input.mousePosition;
            image1.rectTransform.anchoredPosition = startMouse;
            Vector2 delta = stopMouse - startMouse;
            if (delta.x < 0)
            {
                image1.rectTransform.anchoredPosition += new Vector2(delta.x, 0);
                delta.x *= -1;
            }
            if (delta.y < 0)
            {
                image1.rectTransform.anchoredPosition += new Vector2(0, delta.y);
                delta.y *= -1;
            }
            image1.rectTransform.sizeDelta = delta;
        }
        if(Input.GetMouseButtonUp(1))
        {

            for (int i = 0; i < allUnits.Count; i++)
            {
                Rect area = new Rect(image1.rectTransform.anchoredPosition, image1.rectTransform.sizeDelta);
                if (area.Contains(Camera.main.WorldToScreenPoint(allUnits[i].transform.position)))
                {

                    selectedUnits.Add(allUnits[i]);

                }
                image1.enabled = false;
                
            }
        }
    }
}
