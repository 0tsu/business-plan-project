using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damageable))]
public class EnemyController : MonoBehaviour, IHit
{
    Animator anim;
    Rigidbody2D rb2D;
    Damageable damageable;
    TouchingController touchingController;

    [Header("Move Variables")]
    [SerializeField] float speed;
    [SerializeField] Transform player;
    [SerializeField] float minDistace;
    [SerializeField] float maxDistace;

    [Header("Spell Variables")]
    [SerializeField] GameObject spell;
    [SerializeField] Transform spawnSpell;
    float spellSpeed = 8.5f;
    float cooldown;

    

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerControl>().transform;
        damageable = GetComponent<Damageable>();
        touchingController = GetComponent<TouchingController>();
    }

    void Update()
    {
        if (player == null) return;
        Flip();
        if (EnemyDistance() > minDistace && EnemyDistance() < maxDistace)
        {
            EnemyAttack();
            return;
        }
        EnemyMoviment();
        
    }

    void EnemyAttack()
    {
        if(cooldown >= 0.9f)
        {
            GameObject spellObject = Instantiate(spell, spawnSpell.position, spawnSpell.rotation);
            Rigidbody2D rbSpell = spellObject.GetComponent<Rigidbody2D>();
            rbSpell.AddForce((player.position - spellObject.transform.position).normalized * spellSpeed, ForceMode2D.Impulse);
            Destroy(spellObject, 0.5f);
            cooldown = 0f;
        }
        cooldown += Time.deltaTime;
    }

    void EnemyMoviment()
    {
        if (!damageable.canMove || touchingController.IsWalled()) return;
        if(EnemyIsRun() && !damageable.isHit) 
        {
            rb2D.velocity = player.position.x > transform.position.x ? Vector2.left * speed : Vector2.right * speed;
        }
    }
    void Flip()
    {
        float scale = transform.position.x >= player.position.x ? (EnemyIsRun() ? -1f :1f) : -1f;
        transform.localScale = new Vector2(scale, 1f);
    }

    public void OnHit(Vector2 knockBack)

    {
        rb2D.velocity = new Vector2(knockBack.x, rb2D.velocity.y + knockBack.y);
    }

    float EnemyDistance()
    {
        return Vector2.Distance(transform.position, player.position);
    }
    bool EnemyIsRun()
    {
        return EnemyDistance() <= minDistace;
    }
}
