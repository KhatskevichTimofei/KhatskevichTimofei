using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoBox : MonoBehaviour, ISelected, IActivity
{

    public Storage storage;
    public Transform pointSbor;

    public bool IsSelected { get; set; }

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
