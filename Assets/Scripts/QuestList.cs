using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    public string questList;
    public TextMeshProUGUI text;
    public void QuestComplete()
    {
        text.text += " Завершено ";
        
    }
}
