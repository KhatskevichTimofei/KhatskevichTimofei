using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyed : IActivity
{
    void GetDamage(float damage);
    void Destroyed();
}
