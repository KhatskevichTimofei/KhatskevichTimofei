using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestList : UnityEngine.MonoBehaviour
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
        if (quests.Count > 0 && quests[0].inJob)
        {
            text.text = quests[0].questText;
            switch (quests[0].typeQuest)
            {
                case TypeQuest.Destroy:
                    text.text += " :" + (quests[0].all - quests[0].listDestroyed.Count) + " / " + quests[0].all;
                    break;
                case TypeQuest.Research:
                    break;
                case TypeQuest.Create:
                    text.text += " :" + (quests[0].all - quests[0].createNumber) + " / " + quests[0].all;
                    break;
                case TypeQuest.SaveUp:
                    text.text += " :" + Main.instance.storage.plastic + " / " + quests[0].saveUp;
                    break;
            }
            if (quests[0].complete)
            {
                QuestComplete();
            }
        }
    }

    public void QuestComplete()
    {
        quests[0].events.Invoke();
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
        yield return new WaitForSeconds(1.5f);//???
        StartQuest();
    }
}
