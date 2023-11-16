using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IHit
{
    Animator anim;
    Rigidbody2D rb2D;

    [SerializeField] float speed;

    [Header("Attack variables")]
    private bool isAttacking;
    private bool onAttack;

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
    float jumpCount;

    [SerializeField] Transform GroundCollider;
    [SerializeField] LayerMask GroundLayer;
    private bool hit;

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
        anim.SetBool("Attack", onAttack);
        anim.SetBool("JumpFall", jumpCount < 1);
    }


    #region MoveSystem
    public void Move()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        float moveX = xAxis * speed * Time.deltaTime;
        rb2D.velocity = new Vector2(moveX * speed, rb2D.velocity.y);   
    }
    #endregion
    #region JumpSytem


    private bool IsGround()
    {
        return Physics2D.OverlapBox(GroundCollider.position, GroundCollider.localScale, 0f, GroundLayer);
    }
    void PlayerJump()
    {
        if (IsGround()) jumpCount = 1;
        if (Input.GetButtonDown("Jump") && jumpCount >= 1)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            jumpCount--;
        }
    }
    #endregion

    void Flip()
    {
        float scaleX = transform.localScale.x;
        scaleX = xAxis < 0 ? -1 : scaleX;
        scaleX = xAxis > 0 ? 1 : scaleX;
        transform.localScale = new Vector3(scaleX, 1, 1);
    }

#region AttackFunctions

    async void OnFeatherAttackEnded()
    {
        onAttack = false;
        await Task.Delay(100);
        isAttacking = false;
        

    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            isAttacking = true;
            onAttack = true;
            fthrs[indexFeather].Attack();
            SelectNextFeather();
        }

    }
    
    private void SelectNextFeather(){
        indexFeather++;
        indexFeather = indexFeather < fthrs.Count ? indexFeather : 0;
    }

    #endregion
    public void TakeHit()
    {
        Debug.Log(name);
        hit = false;
        rb2D.velocity = new Vector2(10f * transform.localScale.x, 5f);
        hit = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject != gameObject)
            Debug.Log(collision);
            TakeHit();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GroundCollider.position, GroundCollider.localScale);
    }
}