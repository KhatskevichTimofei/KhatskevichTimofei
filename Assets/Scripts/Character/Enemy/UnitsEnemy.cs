using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsEnemy : Character
{
    public override void Destroyed()
    {
        base.Destroyed();
        Main.instance.unitsEnemies.Remove(this);

    }


}
