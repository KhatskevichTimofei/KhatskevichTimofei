using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegoBox : MonoBehaviour, ISelected, IActivity
{
    public Text text;
    public Storage storage;
    public Transform pointSbor;

    public bool IsSelected { get; set; }

    public void Update()
    {
        
    }

    public GameObject GetBox(Unit unit)
    {
        storage.RemovePlastic(50);
        if (storage.plastic <= 0)
        {
            Destroy(gameObject);
        }

        return Instantiate(Resources.Load<GameObject>("Prefabs/Residue/ResidueResource"));
    }


}
