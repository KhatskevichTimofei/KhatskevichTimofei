﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smelter : Build
{
    public List<BabinPrice> prices = new List<BabinPrice>();
    public List<BabinPrice> ochered = new List<BabinPrice>();
    public Transform pointSdachi;

    protected override void Update()
    {
        if (ochered.Count > 0)
        {
            if (ochered[0].AddProgress())
            {
                ochered[0].Complete(this);
                ochered.RemoveAt(0);
            }
        }
        for (int i = 0; i < ocheredImage.Count; i++)
        {
            ocheredImage[i].gameObject.SetActive(i < ochered.Count);
        }
        if (ochered.Count > 0)
        {
            progressBar.transform.parent.gameObject.SetActive(true);
            progressBar.fillAmount = ochered[0].progress / ochered[0].timeCreate;
            //if (i == 5)
            //{

            //}
        }
        else progressBar.transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < prices.Count; i++)
        {
            buttons[i].interactable = Main.instance.storage.ExistBabin(prices[i].babinPrice) && Main.instance.storage.ExistPlastic(prices[i].plasticPrice);
        }
    }



    public void AddOchered(int index)
    {
        AddOchered(prices[index]);
    }


    public void AddOchered(Price price)
    {
        Main.instance.storage.RemovePlastic(price.plasticPrice);
        Main.instance.storage.RemoveBabin(price.babinPrice);
        ochered.Add(price.Copy<BabinPrice>());
    }

    public void QuestStart()
    {
        job = true;
    }
}
