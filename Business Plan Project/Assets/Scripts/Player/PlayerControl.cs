using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator anim;

    [SerializeField] float speed;

    [Header("Attack variables")]
    public bool isAttacking;
    Rigidbody2D rb2D;

    List<Feather> fthrs = new List<Feather>();
    public void AddFeather(Feather fthr)
    {
        fthrs.Add(fthr);
    }

    [SerializeField]int indexFeather;
    //Move variables
    public float xAxis {  get; private set; }

    //Jump variables
    [SerializeField] float jumpForce;
    [SerializeField] float buttonPressedTime;
    [SerializeField] float fallMultiplier;
    [SerializeField] float lowJumpMultiplier;
    [SerializeField] Transform GroundCollider;
    [SerializeField] LayerMask GroundLayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        rb2D = GetComponent<Rigidbody2D>();
    }

#region Events

    void OnEnable(){
        Feather.OnAttackEnd += OnFeatherAttackEnded;
    }

    void OnDisable(){
        Feather.OnAttackEnd -= OnFeatherAttackEnded;
    }

    #endregion

    void Update() 
    {
        Attack();
        Move();
        PlayerJump();
        Flip();
        AnimatorController();
    }

    private void AnimatorController()
    {
        anim.SetBool("Walk", xAxis >= 0f);
    }

#region ControllerFunctions
    #region MoveSystem
    public void Move()
    {
        xAxis = (Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime);
        rb2D.velocity = new Vector2(xAxis * speed, rb2D.velocity.y);
        
    }
    #endregion
    #region JumpSytem
    private bool IsGround()
    {
        return Physics2D.OverlapCircle(GroundCollider.position, 0.1f, GroundLayer);
    }
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && IsGround())
        {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }

        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    #endregion

#endregion

    void Flip()
    {
        float scaleX = transform.localScale.x;
        scaleX = xAxis < 0 ? -1 : scaleX;
        scaleX = xAxis > 0 ? 1 : scaleX;
        transform.localScale = new Vector3(scaleX, 1, 1);
    }

#region AttackFunctions

    void OnFeatherAttackEnded()
    {
        isAttacking = false;
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            isAttacking = true;
            fthrs[indexFeather].Attack();
            SelectNextFeather();
        }

    }
    
    private void SelectNextFeather(){
        indexFeather++;
        indexFeather = indexFeather < fthrs.Count ? indexFeather : 0;
    }

    #endregion
}