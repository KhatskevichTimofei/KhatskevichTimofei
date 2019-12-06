using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour, IDestroyed
{
    public float hp;
    public Animation animationMiach;

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
        if (animationMiach != null)
            animationMiach.Play();
    }

}
