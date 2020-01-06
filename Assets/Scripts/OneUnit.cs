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

    public void HzKakNazvat(Unit unit)
    {
        statsOneUnit.SetActive(true);
        textAttack.text = unit.attack.damage.ToString();
        textHp.text = unit.hp.ToString();
        textSpeed.text = unit.speed.ToString();
        textRA.text = unit.attack.radiusAttack.ToString();

    }
}
