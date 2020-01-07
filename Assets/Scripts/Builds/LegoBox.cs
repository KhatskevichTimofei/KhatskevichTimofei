using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegoBox : MonoBehaviour, ISelected, IActivity
{
    public Text text;
    public Storage storage;
    public Transform pointSbor;
    public GameObject selectedLego;
    public GameObject obvodka;
    public GameObject buildPanelNew;
    public GameObject podlozka;
    protected bool isSelected;
    

    public bool IsSelected {
        get
        {
            return IsSelected;
        }
        set
        {
            isSelected = value;
            if (isSelected)
            {
                selectedLego.SetActive(true);
                obvodka.SetActive(true);
                buildPanelNew.SetActive(true);
                podlozka.SetActive(true);
            }
            else
            {
                selectedLego.SetActive(false);
                obvodka.SetActive(false);
                buildPanelNew.SetActive(false);
                podlozka.SetActive(false);
            }
            isSelected = value;
        }
    }

    public int Prioryti => 0;

    public void OnMouseUp()
    {
       
        Main.instance.selected.Clear();
        Main.instance.selected.Add(this);
        for (int i = 0; i < Main.instance.selected.Count; i++)
        {
            Main.instance.selected[i].IsSelected = true;
        }

    }

    public void Update()
    {
        text.text = storage.plastic.ToString();
    }

    public GameObject GetBox(Unit unit)
    {
        storage.RemovePlastic(50);
        if (storage.plastic <= 0)
        {
            Destroy(gameObject);
        }

        return Instantiate(Resources.Load<GameObject>("Prefabs/Residue/ResidueResource"));
    }


}
