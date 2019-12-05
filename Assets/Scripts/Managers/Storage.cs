using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage
{
    public int babin;
    public float plastic;

    public bool ExistPlastic(float plastic)
    {


        return this.plastic >= plastic;
    }

    public bool ExistBabin(int babin)
    {


        return this.babin >= babin;
    }

    public void AddPlastic(float plastic)
    {
        this.plastic += plastic;
    }

    public void RemovePlastic(float plastic)
    {
        this.plastic -= plastic;
    }

    public void AddBabin(int babin)
    {
        this.babin += babin;
    }

    public void RemoveBabin(int babin)
    {
        this.babin -= babin;
    }

}
