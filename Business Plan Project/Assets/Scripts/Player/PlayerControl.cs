using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb2D;

    [SerializeField] float speed;

    [Header("Attack variables")]
    public bool isAttacking;

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
        anim.SetBool("Walk", Mathf.Abs(xAxis) > 0f);
    }

#region ControllerFunctions
    #region MoveSystem
    public void Move()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        float moveX = xAxis * speed * Time.deltaTime;
        rb2D.velocity = new Vector2(moveX * speed, rb2D.velocity.y);
        
    }
    #endregion
    #region JumpSytem

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GroundCollider.position, GroundCollider.localScale);
    }
    private bool IsGround()
    {
        return Physics2D.OverlapBox(GroundCollider.position, GroundCollider.localScale, 0f, GroundLayer);
    }
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && IsGround())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
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