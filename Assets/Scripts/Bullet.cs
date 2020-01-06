using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : UnityEngine.MonoBehaviour
{
    public float speedBullet;
    public MonoBehaviour parent;

    void Start()
    {
        speedBullet = parent.attack.radiusAttack / (0.3f);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speedBullet;
        Destroy(gameObject, 0.3f);
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        MonoBehaviour character = collider.GetComponent<MonoBehaviour>();
        Interactive interactive = collider.GetComponent<Interactive>();
        if (character != null)
        {
            if (character.sideConflict != parent.sideConflict)
            {
                character.GetDamage(parent.attack.damage);
                Destroy(gameObject);
            }
        }
        if (interactive != null)
        {
                interactive.GetDamage(parent.attack.damage);
                Destroy(gameObject);
        }
    }
}
