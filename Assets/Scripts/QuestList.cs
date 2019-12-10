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
        quests.Clear();
        quests.AddRange(GetComponentsInChildren<Quest>());
        StartQuest();
    }

    void Update()
    {
        if (quests.Count > 0)
        {
            if (quests[0].complete)
            {
                QuestComplete();
            }
            text.text = quests[0].questText;
            switch (quests[0].typeQuest)
            {
                case TypeQuest.Destroy:
                    text.text += (quests[0].listDestroyed.Count - quests[0].allEnemys) + " / " + quests[0].allEnemys;
                    break;
                case TypeQuest.Research:
                    break;
                case TypeQuest.Create:
                    break;
            }
        }
    }

    public void QuestComplete()
    {
        text.text += " Завершено ";
        quests.RemoveAt(0);
        if (quests.Count > 0)
            StartCoroutine(DelayStart());
    }

    public void StartQuest()
    {
        quests[0].OnStart();
        
    }

    public IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1.5f);
        StartQuest();
    }
}
