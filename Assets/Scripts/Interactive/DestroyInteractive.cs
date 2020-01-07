using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInteractive : UnityEngine.MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            character.GetDamage(500);
        }
    }


}
