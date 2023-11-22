using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageHandler : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] bool canDestroy;

    //This is used when it is a projectile such as a feather or spells for example
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable obj = collision.GetComponent<Damageable>();
        if (obj != null)
            obj.Hit(damage, obj);

        if (canDestroy) Destroy(gameObject);
    }
    //This is used when the player receives damage when touching enemies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable obj = collision.gameObject.GetComponent<Damageable>();
        if (obj != null)
        obj.Hit(damage, obj);
    }
}
