using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Build : MonoBehaviour, ISelected, IDestroyed
{
    public float hp;
    public List<Button> buttons = new List<Button>();
    public List<Image> ocheredImage;
    public GameObject ocheredPanel;
    public GameObject funcionalPanel;
    public Image progressBar;
    public Vector3 shortPoint;
    public Vector3 pointSbor;
    protected bool isSelected;
    public SideConflict sideConflict;
    public bool job;
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
                ocheredPanel.SetActive(true);
                funcionalPanel.SetActive(true);
                obvodka.SetActive(true);
            }
            else
            {
                funcionalPanel.SetActive(false);
                ocheredPanel.SetActive(false);
                obvodka.SetActive(false);
            }
            isSelected = value;

        }
    }

    public int Prioryti => 0;

    void Start()
    {

    }


    protected virtual void Update()
    {
    }

    public void OnMouseUp()
    {
        if (!job)
            return;
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
