using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Printer3D : Build
{
    public GameObject ocheredPanel;
    public GameObject funcionalPanel;
    public List<Image> ocheredImage;
    public Image progressBar;
    //public GameObject clickBar

    public override bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value; //
            if (isSelected)
            {
                ocheredPanel.SetActive(true);
                funcionalPanel.SetActive(true);
            }
            else
            {
                funcionalPanel.SetActive(false);
                ocheredPanel.SetActive(false);
            }
            base.IsSelected = value;

        }
    }

    protected override void Update()
    {
        base.Update();
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
        else
            progressBar.transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < unitsPrice.Count; i++)
        {
            buttons[i].interactable = Main.instance.storage.ExistBabin(unitsPrice[i].babinPrice) && Main.instance.storage.ExistPlastic(unitsPrice[i].plasticPrice);
        }
    }
}
