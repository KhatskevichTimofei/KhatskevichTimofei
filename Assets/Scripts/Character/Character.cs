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
    public IDestroyed target;
    public Attack attack;



    public virtual void Update()
    {
        Debug.Log(target);
        Debug.Log((target as MonoBehaviour));
        if (target == null || (target as MonoBehaviour) == null)
            target = null;
        if (target != null)
        {
            Vector3 distance = transform.position - (target as MonoBehaviour).transform.position;
            if (distance.magnitude > attack.radiusAttack)
            {
                SetTargetPosition((target as MonoBehaviour).transform.position + distance.normalized * (attack.radiusAttack - (attack.radiusAttack * 0.1f)));

            }
            else
            {

                if (attack.cdProgress <= 0)
                {
                    Attack(attack, target);
                }
                if (this as Unit != null && (this as Unit).typeTarget == TypeTarget.Set)
                    SetTargetPosition(transform.position);

            }
        }
        attack.cdProgress -= Time.deltaTime;
    }

    public void SetTarget(IDestroyed target)
    {

        this.target = target;

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
