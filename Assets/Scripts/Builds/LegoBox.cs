using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoBox : MonoBehaviour, ISelected, IActivity
{

    public Storage storage;

    public bool IsSelected { get; set; }

    public GameObject GetBox(Unit unit)
    {
        storage.RemovePlastic(50);
        return Instantiate(Resources.Load<GameObject>("Prefabs/Residue/ResidueResource"));

    }


}
