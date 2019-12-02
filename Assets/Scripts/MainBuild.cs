using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainBuild : Build
{
    public Vector3 shortPoint;
    public Vector3 pointSbor;
    void Start()
    {
    }


    void Update()
    {


    }

    public void CreateUnit(int index)
    {
        GameObject obj = Instantiate(unitsPrice[index].unit);
        obj.GetComponent<NavMeshAgent>().Warp(shortPoint);
        obj.GetComponent<Unit>().SetTargetPosition(pointSbor);
        Main.instance.allUnits.Add(obj.GetComponent<Unit>());

    }
}
