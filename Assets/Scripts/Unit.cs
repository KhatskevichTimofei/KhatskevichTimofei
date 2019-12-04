using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : Character, ISelected
{
    
    bool isSelected;

    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value; // ??
            if (isSelected) //Если выбран
            {
                GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/SelectedUnit"); //Загружает материал выбранному юниту
            }
            else
            {
                GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/StandartUnit"); //Загружает материал выбранному юниту
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

}
