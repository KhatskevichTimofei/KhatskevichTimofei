using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Build : MonoBehaviour, ISelected, IDestroyed
{
    public float hp;
    public List<UnitPrice> unitsPrice = new List<UnitPrice>();
    public List<UnitPrice> ochered = new List<UnitPrice>();
    public List<Button> buttons = new List<Button>();
    public Vector3 shortPoint;
    public Vector3 pointSbor;
    protected bool isSelected;
    public GameObject obvodka;

    public virtual bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {

            isSelected = value;
            if (isSelected) 
            {
                obvodka.SetActive(true);
            }
            else
            {
                obvodka.SetActive(false);
            }
        }
    }

    void Start()
    {

    }


    protected virtual void Update()
    {
        if (ochered.Count > 0)
        {
            if (ochered[0].AddProgress())
            {
                CreateUnit(ochered[0].unit);
                ochered.RemoveAt(0);
            }
        }
    }

    public void CreateUnit(GameObject gameObj)
    {
        GameObject obj = Instantiate(gameObj);
        obj.GetComponent<NavMeshAgent>().Warp(shortPoint);
        obj.GetComponent<Unit>().SetTargetPosition(pointSbor);
        Main.instance.allUnits.Add(obj.GetComponent<Unit>());
        Main.instance.allSelectebleObjects.Add(obj.GetComponent<Unit>());
    }

    public void AddOchered(int index)
    {
        AddOchered(unitsPrice[index]);
    }


    public void AddOchered(UnitPrice unitPrice)
    {
        Main.instance.storage.RemovePlastic(unitPrice.plasticPrice);
        Main.instance.storage.RemoveBabin(unitPrice.babinPrice);
        ochered.Add(unitPrice.Copy());

    }

    public void OnMouseUp()
    {
        Main.instance.selected.Clear();
        Main.instance.selected.Add(this);
        for (int i = 0; i < Main.instance.selected.Count; i++)
        {
            Main.instance.selected[i].IsSelected = true;
        }

    }

    public void GetDamage(float damage)
    {

    }

    public void Destroyed()
    {

    }
}
