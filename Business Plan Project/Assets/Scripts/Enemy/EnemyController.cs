using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour, IHit
{
    Animator anim;
    Rigidbody2D rb2D;

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
    [SerializeField]int life;

    
    private bool hit;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerControl>().transform;
    }

    void Update()
    {
        IsDead();
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
        if (!hit)
        {
            if(EnemyIsRun()) 
            {
                rb2D.velocity = player.position.x > transform.position.x ? Vector2.left * speed : Vector2.right * speed;

                return;
            }
        }
    }
    void Flip()
    {
        float scale = transform.position.x >= player.position.x ? (EnemyIsRun() ? -1f :1f) : -1f;
        transform.localScale = new Vector2(scale, 1f);
    }
    float EnemyDistance()
    {
        return Vector2.Distance(transform.position, player.position);
    }
    bool EnemyIsRun()
    {
        return EnemyDistance() <= minDistace;
    }

    void IsDead()
    {
        if (life <= 0)
        {
            Debug.Log("Eu morri");
            Destroy(gameObject, 0.2f);
        }
    }

    async public void TakeHit()
    {
        Debug.Log(name);
        life--;
        rb2D.AddForce(new Vector2((EnemyIsRun() ? -5f : 5f) * transform.localScale.x, 5f), ForceMode2D.Impulse);
        Debug.Log(life);
        hit = true;
        await Task.Delay(1000);
        hit = false;
    }
}
