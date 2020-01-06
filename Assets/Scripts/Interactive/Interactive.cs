using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypePushAndAttack
{
    Push,
    Attack
}
public class Interactive : MonoBehaviour, IDestroyed
{
    public float hp;
    public Animation anim;
    public TypePushAndAttack typePushAndAttack;
    public Animation selectAnim;
    public bool job;
    public int kubokOff = 0;

    public void GetDamage(float damage)
    {
        if (!job)
            return;
        hp -= damage;
        if (hp <= 0)
        {
            Destroyed();
        }

    }

    public virtual void Destroyed()
    {
        switch (typePushAndAttack)
        {
            case TypePushAndAttack.Push:
                if (kubokOff == 0)
                {
                    if (anim != null)
                        anim.Play();
                    if (selectAnim != null)
                        selectAnim.Play();
                    kubokOff = 1;
                }
                break;
            case TypePushAndAttack.Attack:
                Destroy(gameObject);
                if (anim != null)
                    anim.Play();
                break;
        }
    }

    public void KubokOn()
    {
        job = true;
    }

}
