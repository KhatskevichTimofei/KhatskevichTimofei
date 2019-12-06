using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BabinPrice : Price
{
    public int result;

    public override void Complete(Build build)
    {
        Main.instance.storage.AddBabin(result);
    }

    public override T Copy<T>()
    {
        BabinPrice price = new BabinPrice
        {
            result = result,
            timeCreate = timeCreate,
            plasticPrice = plasticPrice,
            babinPrice = babinPrice,
            infoUnit = infoUnit
        };
        return price as T;
    }
}
