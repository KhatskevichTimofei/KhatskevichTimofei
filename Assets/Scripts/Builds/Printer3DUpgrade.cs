using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer3DUpgrade : Build, IDestroyed, ISelected
{
    public List<UnitPrice> prices = new List<UnitPrice>();
    public List<UnitPrice> ochered = new List<UnitPrice>();
    public Animation anim;
    //public GameObject clickBar


    protected override void Update()
    {
        if (ochered.Count > 0)
        {
            //if (!anim.isPlaying)
            //{
            //    anim.Play();
            //}
            if (ochered[0].AddProgress())
            {
                ochered[0].Complete(this);
                ochered.RemoveAt(0);
            }
        }
        else anim.Stop();
        for (int i = 0; i < ocheredImage.Count; i++)
        {
            ocheredImage[i].gameObject.SetActive(i < ochered.Count);
        }
        if (ochered.Count > 0)
        {
            progressBar.transform.parent.gameObject.SetActive(true);
            progressBar.fillAmount = ochered[0].progress / ochered[0].timeCreate;
        }
        else progressBar.transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < prices.Count; i++)
        {
            buttons[i].interactable = Main.instance.storage.ExistBabin(prices[i].babinPrice) && Main.instance.storage.ExistPlastic(prices[i].plasticPrice);
        }
    }



    public void AddOchered(int index)
    {
        if (ochered.Count == 0)
        {
            anim.Play();
        }
        if (ochered.Count < 5)
            AddOchered(prices[index]);

    }


    public void AddOchered(Price price)
    {
        Main.instance.storage.RemovePlastic(price.plasticPrice);
        Main.instance.storage.RemoveBabin(price.babinPrice);
        ochered.Add(price.Copy<UnitPrice>());
    }



    public void QuestStart()
    {
        job = true;
    }


}
