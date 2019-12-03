using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Build : MonoBehaviour, ISelected
{
    public float hp;
    public List<UnitPrice> unitsPrice = new List<UnitPrice>();

    public bool IsSelected { get; set; }

    void Start()
    {

    }


    void Update()
    {

    }
}
