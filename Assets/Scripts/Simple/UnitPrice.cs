using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitPrice
{
    public GameObject unit;
    public float timeCreate;
    public int plasticPrice, babinPrice;
    public string infoUnit;
    public float progress;


    public bool AddProgress()
    {

        progress += Time.deltaTime;
        return progress >= timeCreate;
    }

    public UnitPrice Copy()
    {

        UnitPrice unitPrice = new UnitPrice
        {
            unit = unit,
            timeCreate = timeCreate,
            plasticPrice = plasticPrice,
            babinPrice = babinPrice,
            infoUnit = infoUnit
        };
        return unitPrice;
    }
}
