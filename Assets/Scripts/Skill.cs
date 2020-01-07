using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skills
{
    DamageBuff,
    HpBuff
}
public class Skill : MonoBehaviour
{
    public Sprite spriteSkill;
    public float cdSkill, timeAction;
    public Skills skills;

    void Update()
    {
        if (cdSkill > 0)
            cdSkill -= Time.deltaTime;
        if (timeAction > 0)
            timeAction -= Time.deltaTime;
    }
    public void Buff(Unit unit)
    {

        for (int i = 0; i < Main.instance.selected.Count; i++)
        {
            if (cdSkill <= 0)
            {
                (Main.instance.selected[i] as Character).attack.damage += 1000;
                cdSkill = 3;
            }
        }
    }
}
