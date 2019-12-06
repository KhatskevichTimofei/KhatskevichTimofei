using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Price 
{
    public float timeCreate;
    public int plasticPrice, babinPrice;
    public string infoUnit;
    public float progress;

    public bool AddProgress()
    {

        progress += Time.deltaTime;
        return progress >= timeCreate;
    }

    public abstract void Complete(Build build);

    public abstract T Copy<T>() where T : Price;

}
