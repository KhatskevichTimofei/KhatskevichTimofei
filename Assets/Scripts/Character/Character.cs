using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum SideConflict
{
    Player,
    Enemy
}


public class Character : MonoBehaviour, IDestroyed
{
    public float hp, speed, armor, radiusLook;
    public NavMeshAgent agent;
    public SideConflict sideConflict;
    public Character target;
    public Attack attack;



    public virtual void Update()
    {
        if (target != null)
        {
            Vector3 distance = transform.position - target.transform.position;
            if (distance.magnitude > attack.radiusAttack)
            {
                SetTargetPosition(target.transform.position + distance.normalized * (attack.radiusAttack - (attack.radiusAttack * 0.1f)));

            }
            else
            {

                if (attack.cdProgress <= 0)
                {
                    Attack(attack, target);
                }

            }
        }
        attack.cdProgress -= Time.deltaTime;
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

    public void Attack(Attack attack, IDestroyed destroyed)
    {
        destroyed.GetDamage(attack.damage);
        attack.cdProgress = attack.cd;
    }

    public void GetDamage(float damage)
    {

        hp -= damage;
        if (hp <= 0)
        {
            Destroyed();
        }

    }

    public virtual void Destroyed()
    {
        Destroy(gameObject);
    }
}
