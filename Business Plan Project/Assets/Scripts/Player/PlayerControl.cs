using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : Character, IMove, IAttack
{
    
    [SerializeField] FeatherAnimation[] Fthr;
    [SerializeField] int indexFeather;
    [Header("Move variables")]
    public float xAxis;
    
    [Header("Jump variables")]
    [SerializeField] float jumpForce;
    [SerializeField] Transform GroundCollider;
    [SerializeField] LayerMask GroundLayer;

    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        Attack();
        PlayerJump();
        Move();
        Flip();
    }
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !onPAttack)
        {
            StartCoroutine(AttackTime());
        }
    }
    
    public void Move()
    {
        if (isAttacking) return;
        xAxis = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        rb2D.velocity = new Vector2(xAxis * speed, rb2D.velocity.y); 
    }
    bool IsGround()
    {
        return Physics2D.OverlapCircle(GroundCollider.position, 0.1f, GroundLayer);
    }
    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }
    }

    void Flip()
    {
        
        float scaleX = transform.localScale.x;
        scaleX = xAxis < 0 ? -1 : scaleX;
        scaleX = xAxis > 0 ? 1 : scaleX;
        transform.localScale = new Vector3(scaleX, 1, 1);
    }

    public IEnumerator AttackTime()
    {
        onPAttack = true;
        Fthr[indexFeather].onFAttack = true;
        
        yield return new WaitForSeconds(attackTime);
        Fthr[indexFeather].onFAttack = false;
        
        yield return new WaitForSeconds(cooldownAttack);
        onPAttack = false;

        indexFeather++;
        if (indexFeather >= Fthr.Length)
            indexFeather = 0;
    }
}