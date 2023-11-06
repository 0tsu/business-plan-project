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
    [SerializeField] float mediumDistace;
    [SerializeField] float maxDistace;
    float enemyDistance;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerControl>().transform;
    }

    void Update()
    {
        EnemyMoviment();
        
    }
    void EnemyMoviment()
    {
        enemyDistance = Vector2.Distance(transform.position, player.position);
           
        if(enemyDistance <= minDistace) 
        {
            rb2D.velocity = player.position.x > transform.position.x ? Vector2.left * speed : Vector2.right * speed;
            Flip(-1);
            return;
        }
        Flip(1);
    }
    void Flip(float DirectionFlip)
    {
        float scale = transform.position.x >= player.position.x ? 1.0f * DirectionFlip : -1.0f * DirectionFlip;
        transform.localScale = new Vector2(scale, 1f);
    }
}
