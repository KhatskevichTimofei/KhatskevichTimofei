using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : MonoBehaviour, ISelected
{
    public float hp, damage, speed, armor, speedShots;
    public NavMeshAgent agent;
    bool isSelected;

    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;
            if (isSelected)
            {
                GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/SelectedUnit");
            }
            else
            {
                GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/StandartUnit");
            }

        }
    }


    void Start()
    {

    }

    public virtual void Update()
    {

    }


    public void SetTargetPosition(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(position, path))
        {
            agent.SetPath(path);
        }

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

}
