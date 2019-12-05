using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : Character, ISelected
{
    public GameObject obvodka;
    bool isSelected;

    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value; // ??
            if (isSelected) //Если выбран
            {
                //GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/SelectedUnit"); //Загружает материал выбранному юниту
                obvodka.SetActive(true);
            }
            else
            {
                //GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/StandartUnit"); //Загружает материал выбранному юниту
                obvodka.SetActive(false);
            }

        }
    }


    void Start()
    {

    }

    public override void Update()
    {
        base.Update();

    }




    public void OnMouseUp()  //При нажатии на мышь отчищается выбранные юниты. Добавляет в список выбранных ???. Проходится по циклу от i до размерности листа выбранных. И каждый выбранный юнит получает статус выделенного.
    {
        Main.instance.selected.Clear();
        Main.instance.selected.Add(this);
        for (int i = 0; i < Main.instance.selected.Count; i++)
        {
            Main.instance.selected[i].IsSelected = true;
        }

    }

    public override void Destroyed()
    {
        base.Destroyed();
        Main.instance.allUnits.Remove(this);
        if (Main.instance.selected.Contains(this))
            Main.instance.selected.Remove(this);
        Main.instance.allSelectebleObjects.Remove(this);

    }

}
