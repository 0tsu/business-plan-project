using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : Character
{

    List<Feather> fthrs = new List<Feather>();
    public void AddFeather(Feather fthr)
    {
        fthrs.Add(fthr);
    }

    int indexFeather;
    //Move variables
    public float xAxis {  get; private set; }
    
    //Jump variables
    [SerializeField] float jumpForce;
    [SerializeField] Transform GroundCollider;
    [SerializeField] LayerMask GroundLayer;

    void Start()
    {

        Application.targetFrameRate = 60;
        //QualitySettings.vSyncCount = 0;
        rb2D = GetComponent<Rigidbody2D>();
    }
    void OnEnable(){
        Feather.OnAttackEnd += OnFeatherAttackEnded;
    }
    void OnDisable(){
        Feather.OnAttackEnd -= OnFeatherAttackEnded;
    }
    void OnFeatherAttackEnded()
    {
        isAttacking = false;
    }

    void Update() 
    {
        Attack();
        PlayerJump();
        Move();
        Flip();
    }
    
    public void Move()
    {
        xAxis = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        Vector2 move = new Vector2(xAxis * speed, rb2D.velocity.y); 
        rb2D.velocity = move;
    }
    bool IsGround()
    {
        return Physics2D.OverlapCircle(GroundCollider.position, 0.1f, GroundLayer);
    }
    void PlayerJump()
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
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            // StartCoroutine(Attacking(attackTime));
            isAttacking = true;
            fthrs[indexFeather].Attack();
            SelectNextFeather();
        }

    }
    
    private void SelectNextFeather(){
        indexFeather++;
        indexFeather = indexFeather < fthrs.Count ? indexFeather : 0;
    }
}