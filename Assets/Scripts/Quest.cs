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
    public List<IDestroyed> listDestroyed;
    public int createNumber;
    public bool complete;
    public string questText;
    public bool inJob;

    void Update()
    {
        switch (typeQuest)
        {
            case TypeQuest.Destroy:
                for (int i = 0; i < listDestroyed.Count; i++)
                {
                    if (listDestroyed[i] != null)
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

    private void OnTriggerEnter(Collider other)
    {

        if (inJob && other.GetComponent<Unit>() != null)
        {
            complete = true;
            Destroy(this);
        }

    }
}
