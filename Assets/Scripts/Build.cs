using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Build : MonoBehaviour, ISelected
{
    public float hp;
    public List<UnitPrice> unitsPrice = new List<UnitPrice>();
    public List<UnitPrice> ochered = new List<UnitPrice>();
    public Vector3 shortPoint;
    public Vector3 pointSbor;

    public bool IsSelected { get; set; }

    void Start()
    {

    }


    void Update()
    {
        if (ochered.Count > 0)
        {
            if (ochered[0].AddProgress())
            {
                CreateUnit(ochered[0].unit);
                ochered.RemoveAt(0);
            }
        }
    }

    public void CreateUnit(GameObject gameObj)
    {
        GameObject obj = Instantiate(gameObj);
        obj.GetComponent<NavMeshAgent>().Warp(shortPoint);
        obj.GetComponent<Unit>().SetTargetPosition(pointSbor);
        Main.instance.allUnits.Add(obj.GetComponent<Unit>());
        Main.instance.allSelectebleObjects.Add(obj.GetComponent<Unit>());
    }

    public void AddOchered(int index)
    {
        AddOchered(unitsPrice[index]);
    }


    public void AddOchered(UnitPrice unitPrice)
    {
        ochered.Add(unitPrice.Copy());

    }
}
