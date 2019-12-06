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
    public IActivity target;
    public Attack attack;
    public CollectionUP collectionUP;
    public Transform collectionUpParent;
    //public Attack radiusUpCharacter;



    public virtual void Update()
    {

        if (target == null || (target as MonoBehaviour) == null)
            target = null;
        if (target != null)
        {
            CollectionUP collection = target as CollectionUP;
            IDestroyed destroyed = target as IDestroyed;


            Vector3 distance = transform.position - (target as MonoBehaviour).transform.position;
            if (destroyed != null && distance.magnitude > attack.radiusAttack)
            {
                SetTargetPosition((target as MonoBehaviour).transform.position + distance.normalized * (attack.radiusAttack - (attack.radiusAttack * 0.1f)));

            }
            else
            {
                if (destroyed != null)
                {
                    if (attack.cdProgress <= 0)
                    {
                        Attack(attack, destroyed);
                    }
                    if (this as Unit != null && (this as Unit).typeTarget == TypeTarget.Set)
                        SetTargetPosition(transform.position);
                }
                else
                {
                    if (collection != null)
                    {
                        SetTargetPosition(collection.transform.position);
                        if (distance.magnitude < 2) //Длина 
                        {
                            collectionUP = collection;
                            collection.transform.parent = collectionUpParent;
                            collection.transform.localPosition = new Vector3(0, 0, 0);
                            SetTarget(null);
                            (this as Unit).typeTarget = TypeTarget.Auto;
                        }
                    }

                }

            }
        }
        attack.cdProgress -= Time.deltaTime;
    }

    public void SetTarget(IActivity target)
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
        GameObject residue = Instantiate(Resources.Load<GameObject>("Prefabs/Residue/ResidueResource"));
        Destroy(gameObject);
        residue.transform.position = transform.position;
        
    }

}
