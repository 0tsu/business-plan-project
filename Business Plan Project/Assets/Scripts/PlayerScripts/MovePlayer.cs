using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Rigidbody2D Rb2D;
    [SerializeField] Feathers[] fthrs;
    [SerializeField] float speedPlayer; //Variavel flutuante responsavel em definir a velocidade do player
    [SerializeField] float jumpForce; //Variavel flutuante responsavel em definir a velocidade de pulo do player
    [SerializeField] Transform GroundCollider;
    [SerializeField] LayerMask GroundLayer;
    public float xAxis { get; set; } //Variavel flutuante responsavel em definir a direção do player
    public bool isAttacking;
    [SerializeField] bool onAttack;
    public int side { get; set; }

    [SerializeField] float attackTime;
    [SerializeField] float cooldownAttack;
    public int currentFeatherIndex = 0;

    void Start() //Executa apenas uma vez quando inicia o jogo
    {
        side = 1;
        Application.targetFrameRate = 60; //Codigo que tem como objetivo travar o jogo em 60 quadros por segundo
        QualitySettings.vSyncCount = 0; //Codigo responsavel em desativar o vSync
        Rb2D = GetComponent<Rigidbody2D>(); //Codigo responsavel em pegar os componetes da classe "Rigidbody2D"
        fthrs = FindObjectsOfType<Feathers>();
    }
    void Update() //Executa a todo momento
    {
        Attack();
        PlayerJump(); //Metodo responsavel em fazer o sistema de pulo
        PlayerMove(); //Metodo responsavel por mover o player
        FlipPlayer(); //Metodo responsavel por virar o player de acordo com a direção do mesmo
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !onAttack)
        {
            StartCoroutine(AttackTime());
        }
    }
    
    private void PlayerMove()
    {
        if (isAttacking) return;
        xAxis = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speedPlayer; //define a direção e a velociade do jogador

        Rb2D.velocity = new Vector2(xAxis * speedPlayer, Rb2D.velocity.y); //Move o player
    }
    bool IsGround() //Metodo boleano responsavel em retornar verdadeiro se o player estiver no chão e falso se ele estiver no ar
    {
        return Physics2D.OverlapCircle(GroundCollider.position, 0.1f, GroundLayer);
    }
    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGround()) //Condição caso o jogador aperte o espaço e se o player estiver no chão
        {
            Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce); //Define uma velocidade vertical para dar a sensação de pulo
        }
    }

    private void FlipPlayer()
    {
        transform.localScale = new Vector3(side, 1, 1);
        //Se o player apertar a tecla "A" ou a seta da esquerda ele vai virar o player para esquerda
        side = xAxis < 0 ? -1 : side;
        //Caso ele aperte a tecla "D" ou a seta pra direita ele vai virar o player para direita
        side = xAxis > 0 ? 1 : side;
    }

    private IEnumerator AttackTime()
    {
        onAttack = true;
        int currentAttackIndex = currentFeatherIndex;
        fthrs[currentAttackIndex].onAttackFeather = true;
        fthrs[currentFeatherIndex].ToggleTrainRender(true);
        fthrs[currentAttackIndex].AttackFeather();
        yield return new WaitForSeconds(attackTime);
        fthrs[currentAttackIndex].onAttackFeather = false;
        fthrs[currentFeatherIndex].ToggleTrainRender(false);
        yield return new WaitForSeconds(cooldownAttack);
        onAttack = false;
        currentFeatherIndex++;
        if(currentFeatherIndex >= fthrs.Length)
            currentFeatherIndex = 0;
    }
}