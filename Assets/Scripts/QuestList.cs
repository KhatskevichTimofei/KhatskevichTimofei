using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    public TextMeshProUGUI text;
    public List<Quest> quests;

    void Start()
    {
        StartQuest();
    }

    void Update()
    {
        if (quests.Count > 0)
            if (quests[0].complete)
            {
                QuestComplete();
                quests.RemoveAt(0);
                if (quests.Count > 0)
                    StartQuest();
            }
    }

    public void QuestComplete()
    {
        text.text += " Завершено ";

    }

    public void StartQuest()
    {
        text.text = quests[0].questText;
        quests[0].inJob = true;
    }
}
