using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Storage
{
    public int babin;
    public float plastic;
    public Image babinIcon;
    public Image plasticIcon;
    public TextMeshProUGUI textBabin;
    public TextMeshProUGUI textPlastic;

    public void Update()
    {
        textBabin.text = babin.ToString();
        textPlastic.text = plastic.ToString();
    }

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
