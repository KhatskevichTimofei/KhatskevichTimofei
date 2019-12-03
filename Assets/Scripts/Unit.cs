using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : MonoBehaviour, ISelected
{
    public float hp, damage, speed, armor, speedShots;
    public NavMeshAgent agent;



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
    }
    
}
