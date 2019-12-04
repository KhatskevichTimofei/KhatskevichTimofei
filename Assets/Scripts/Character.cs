using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum SideConflict
{
    Player,
    Enemy
}


public class Character : MonoBehaviour
{
    public float hp, damage, speed, armor, speedShots, radiusLook;
    public NavMeshAgent agent;
    public SideConflict sideConflict;
    public Character target;



    public virtual void Update()
    {
        if(target != null)
        SetTargetPosition(target.transform.position);
        
    }

    public void SetTarget(Character character)
    {

        target = character;

    }

    public void SetTargetPosition(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(position, path))
        {
            agent.SetPath(path);
        }

    }
}
