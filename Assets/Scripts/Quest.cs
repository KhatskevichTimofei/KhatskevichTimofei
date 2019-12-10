using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeQuest
{
    Destroy,
    Research,
    Create
}
public class Quest : MonoBehaviour
{
    public TypeQuest typeQuest;
    public List<Character> listDestroyed;
    public int createNumber;
    public bool complete;
    public string questText;
    public bool inJob;
    public int allEnemys;

    void Awake()
    {
        Unit.onCreate += Create;
    }

    void Create()
    {
        if (!inJob)
            return;
        if (typeQuest == TypeQuest.Create)
        {
            createNumber--;
            if (createNumber == 0)
            {
                complete = true;
            }
        }
    }

    void Update()
    {
        if (!inJob)
            return;
        switch (typeQuest)
        {
            case TypeQuest.Destroy:
                for (int i = 0; i < listDestroyed.Count; i++)
                {
                    if (listDestroyed[i] == null)
                    {
                        listDestroyed.RemoveAt(i);
                    }
                }
                if (listDestroyed.Count == 0)
                {
                    complete = true;
                }
                break;
            case TypeQuest.Research:


                break;
            case TypeQuest.Create:

                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (inJob && other.GetComponent<Unit>() != null)
        {
            complete = true;
            Destroy(this);
        }
    }

    public void OnStart()
    {
        inJob = true;
        allEnemys = listDestroyed.Count;
    }



}
