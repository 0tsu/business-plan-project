using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damageable), typeof(TouchingController))]
public class PlayerControl : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb2D;
    Damageable damageable;
    TouchingController touchingController;


    [Header("Attack variables")]
    private bool canAttacking;

    List<Feather> fthrs = new List<Feather>();
    public void AddFeather(Feather fthr)
    {
        fthrs.Add(fthr);
    }

    [SerializeField] int indexFeather;
    //Move variables
    [SerializeField] float speed;
    float xAxis;
    float moveX;

    //Jump variables
    [SerializeField] float jumpForce;




    
    public bool isAlive { get { return anim.GetBool(AnimationString.IsAlive); } }
    

    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
        touchingController = GetComponent<TouchingController>();
    }

    #region Events

    void OnEnable() {
        Feather.OnAttackEnd += OnFeatherAttackEnded;

    }

    void OnDisable() {
        Feather.OnAttackEnd -= OnFeatherAttackEnded;
    }

    #endregion

    void Update()
    {
        if (!damageable.isAlive) return;
        Move();
        Attack();
        PlayerJump();
        Flip();
        AnimatorController();
    }

    private void AnimatorController()
    {
        anim.SetBool(AnimationString.IsGround, touchingController.IsGround());
        anim.SetFloat(AnimationString.Yvelocity, rb2D.velocity.y);
    }


    #region MoveSystem
    public void Move()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        moveX = xAxis * speed * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        anim.SetBool(AnimationString.Walk, Mathf.Abs(xAxis) > 0f);
        if (!damageable.canMove || touchingController.IsWalled()) return;
        if (!damageable.isHit)
        {
            rb2D.velocity = new Vector2(moveX * speed, rb2D.velocity.y);
        }


    }
    #endregion
    #region JumpSytem
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && (touchingController.IsGround()))
        {
            anim.SetTrigger(AnimationString.Jump);
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }
        
    }
    #endregion

    void Flip()
    {
        float scaleX = transform.localScale.x;
        scaleX = xAxis < 0 ? -1 : scaleX;
        scaleX = xAxis > 0 ? 1 : scaleX;
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

#region AttackFunctions

    async void OnFeatherAttackEnded()
    {
        await Task.Delay(200);
        canAttacking = false;
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !canAttacking)
        {
            anim.SetTrigger(AnimationString.Attack);
            canAttacking = true;
            fthrs[indexFeather].Attack();
            SelectNextFeather();
        }

    }
    
    private void SelectNextFeather(){
        indexFeather++;
        indexFeather = indexFeather < fthrs.Count ? indexFeather : 0;
    }

    #endregion


    public void OnHit(Vector2 knockBack)
    {
        rb2D.velocity = new Vector2(knockBack.x, rb2D.velocity.y + knockBack.y);
    }


}