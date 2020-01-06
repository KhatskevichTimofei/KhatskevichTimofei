using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInteractive : UnityEngine.MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MonoBehaviour character = other.GetComponent<MonoBehaviour>();
        if (character != null)
        {
            character.GetDamage(500);
        }
    }


}
