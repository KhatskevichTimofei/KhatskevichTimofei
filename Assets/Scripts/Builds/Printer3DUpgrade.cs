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
        else anim.Stop(); //Останавливает анимацию
        for (int i = 0; i < ocheredImage.Count; i++) //Активирует на панеле картинку если в очереди кто-то есть
        {
            ocheredImage[i].gameObject.SetActive(i < ochered.Count);
        }
        if (ochered.Count > 0)//Если в очереди есть кто-то, то запускается запускается ProgressBar и ???
        {
            progressBar.transform.parent.gameObject.SetActive(true);
            progressBar.fillAmount = ochered[0].progress / ochered[0].timeCreate;
        }
        else progressBar.transform.parent.gameObject.SetActive(false); //Иначе отключает ProgressBar 
        for (int i = 0; i < prices.Count; i++)//Кнопка становится активной после того как нам хватает бабина и пластика
        {
            buttons[i].interactable = Main.instance.storage.ExistBabin(prices[i].babinPrice) && Main.instance.storage.ExistPlastic(prices[i].plasticPrice);
        }
    }



    public void AddOchered(int index)
    {
        if (ochered.Count == 0)//Если размер листа равен 0, то запускается анимация ???
        {
            anim.Play();
        }
        if (ochered.Count < 5) //Если размер очереди меньше 6 ???
            AddOchered(prices[index]);

    }


    public void AddOchered(Price price)
    {
        Main.instance.storage.RemovePlastic(price.plasticPrice); //
        Main.instance.storage.RemoveBabin(price.babinPrice);
        ochered.Add(price.Copy<UnitPrice>());
    }



    public void QuestStart()
    {
        job = true;
    }


}
