using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneUnit : MonoBehaviour
{
    public TextMeshProUGUI textHp, textAttack, textSpeed, textRA;
    public GameObject statsOneUnit;
    public List<Button> standartCommand;
    public List<Button> skills;
    public Image iconka;

    public void UnitStats(Unit unit)
    {
        if (unit == null)
            return;
        if (Main.instance.selected.Count == 1)
        {
            statsOneUnit.SetActive(true);
            textAttack.text = unit.attack.damage.ToString();
            textHp.text = unit.hp.ToString();
            textSpeed.text = unit.speed.ToString();
            textRA.text = unit.attack.radiusAttack.ToString();
            iconka.sprite = unit.Icon;
        }
        else if (Main.instance.selected.Count > 1) 
            statsOneUnit.SetActive(false);

    }

    public void ButtonsSelectUnit(Unit unit)
    {
        skills.Clear();
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].gameObject.SetActive(true);
        }

    }
}
