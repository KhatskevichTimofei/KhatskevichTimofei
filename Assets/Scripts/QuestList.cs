using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    public string questList;
    public TextMeshProUGUI text;
    public List<Quest> quests;
    public bool inJob;

    void Start()
    {
        
    }
    public void QuestComplete()
    {
        text.text += " Завершено ";

    }

    public void StartQuest()
    {
        text.text = quests[0].questText;
        //quests[0].inJob =;
    }
}
