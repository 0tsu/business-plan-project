using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb2D;

    [SerializeField] float speed;
    [SerializeField] Transform player;
    [SerializeField] float minDistace;
    [SerializeField] float maxDistace;
    [SerializeField] Transform spawnSpell;
    [SerializeField] GameObject spell;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerControl>().transform;
    }

    void Update()
    {
        if(EnemyDistance() > minDistace && EnemyDistance() < maxDistace)
        {
            EnemyAttack();
            return;
        }
        EnemyMoviment();
        
    }

    void EnemyAttack()
    {
        GameObject spellObj = Instantiate(spell, spawnSpell.position, spawnSpell.rotation);
        Rigidbody2D spellRig = spellObj.GetComponent<Rigidbody2D>();
        spellRig.AddForce(spellRig.transform.forward);
        Destroy(spellObj, 0.1f);
    }

    void EnemyMoviment()
    {  
        if(EnemyDistance() <= minDistace) 
        {
            rb2D.velocity = player.position.x > transform.position.x ? Vector2.left * speed : Vector2.right * speed;
            Flip(-1);
            return;
        }
        Flip(1);
    }
    void Flip(float DirectionFlip)
    {
        float scale = transform.position.x >= player.position.x ? 1f * DirectionFlip : -1f * DirectionFlip;
        transform.localScale = new Vector2(scale, 1f);
    }
    float EnemyDistance()
    {
        return Vector2.Distance(transform.position, player.position);
    }
}
