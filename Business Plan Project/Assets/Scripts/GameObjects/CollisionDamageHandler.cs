using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageHandler : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] bool canDestroy;
    [SerializeField] Vector2 knockBack = Vector2.zero;


    //This is used when it is a projectile such as a feather or spells for example
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDealDamage(collision.gameObject);
    }
    //This is used when the player receives damage when touching enemies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryDealDamage(collision.gameObject);
    }
    void TryDealDamage(GameObject collidedObject)
    {
        Damageable obj = collidedObject.GetComponent<Damageable>();
        if (obj != null)
            obj.Hit(damage, knockBack);

        if (canDestroy) Destroy(gameObject);
    }
}
