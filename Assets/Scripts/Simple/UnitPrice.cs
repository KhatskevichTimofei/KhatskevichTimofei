using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class UnitPrice : Price
{
    public GameObject unit;



    public override void Complete(Build build)
    {
        GameObject obj = Object.Instantiate(unit);
        obj.GetComponent<NavMeshAgent>().Warp(build.shortPoint);
        obj.GetComponent<Unit>().SetTargetPosition(build.pointSbor);
        obj.GetComponent<Unit>().typeTarget = TypeTarget.Set;
        Main.instance.allUnits.Add(obj.GetComponent<Unit>());
        Main.instance.allSelectebleObjects.Add(obj.GetComponent<Unit>());
    }

    public override T Copy<T>()
    {
        UnitPrice unitPrice = new UnitPrice
        {
            unit = unit,
            timeCreate = timeCreate,
            plasticPrice = plasticPrice,
            babinPrice = babinPrice,
            infoUnit = infoUnit
        };
        return unitPrice as T;
    }
}
