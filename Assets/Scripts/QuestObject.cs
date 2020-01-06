using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : UnityEngine.MonoBehaviour
{
    public QuestList questList;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>() != null)
        {
            questList.QuestComplete();
            Destroy(gameObject);
        }

    }
}
