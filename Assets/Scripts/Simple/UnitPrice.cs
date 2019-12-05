using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitPrice
{
    public GameObject unit;
    public float timeCreate;
    public int plasticPrice, colorPrice;
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
            colorPrice = colorPrice,
            infoUnit = infoUnit
        };
        return unitPrice;
    }
}
