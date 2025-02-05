﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : UnitsEnemy
{
    public override void Update()
    {
        base.Update();
        float minDistance = float.MaxValue;
        Unit unit = null;
        for (int i = 0; i < Main.instance.allUnits.Count; i++)
        {
            float distance = (Main.instance.allUnits[i].transform.position - transform.position).magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                unit = Main.instance.allUnits[i];
            }
        }
        if (minDistance < radiusLook)
        {
            SetTarget(unit);
            //AudioManager.AddAudio(transform.position, /*"TaracanGo"*/);
        }
    }

    public override void Destroyed()
    {
        base.Destroyed();
    }
}
 
