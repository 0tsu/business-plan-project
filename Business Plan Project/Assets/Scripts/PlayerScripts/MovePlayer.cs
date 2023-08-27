using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Rigidbody2D Rb2D;
    [SerializeField] float speedPlayer; //Variavel flutuante responsavel em definir a velocidade do player
    [SerializeField] float jumpForce; //Variavel flutuante responsavel em definir a velocidade de pulo do player
    [SerializeField] Transform GroundCollider;
    [SerializeField] LayerMask GroundLayer;
    public float xAxis { get; set; } //Variavel flutuante responsavel em definir a dire��o do player
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
        //fthrs = FindObjectsOfType<Feather>();
    }
    void Update() //Executa a todo momento
    {
        Attack();
        PlayerJump(); //Metodo responsavel em fazer o sistema de pulo
        PlayerMove(); //Metodo responsavel por mover o player
        FlipPlayer(); //Metodo responsavel por virar o player de acordo com a dire��o do mesmo
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !onAttack)
        {
            //StartCoroutine(AttackTime());
        }
    }
    
    private void PlayerMove()
    {
        if (isAttacking) return;
        xAxis = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speedPlayer; //define a dire��o e a velociade do jogador

        Rb2D.velocity = new Vector2(xAxis * speedPlayer, Rb2D.velocity.y); //Move o player
    }
    bool IsGround() //Metodo boleano responsavel em retornar verdadeiro se o player estiver no ch�o e falso se ele estiver no ar
    {
        return Physics2D.OverlapCircle(GroundCollider.position, 0.1f, GroundLayer);
    }
    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGround()) //Condi��o caso o jogador aperte o espa�o e se o player estiver no ch�o
        {
            Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce); //Define uma velocidade vertical para dar a sensa��o de pulo
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

    /*private IEnumerator AttackTime()
    {
        onAttack = true;
        int currentAttackIndex = currentFeatherIndex;
        fthrs[currentAttackIndex].onAttackFeather = true;
        fthrs[currentFeatherIndex].ToggleTrainRender(true);
        fthrs[currentAttackIndex].AttackFeather();
        yield return new WaitForSeconds(attackTime);
        fthrs[currentAttackIndex].onAttackFeather = false;
        yield return new WaitForSeconds(cooldownAttack);
        fthrs[currentFeatherIndex].ToggleTrainRender(false);
        onAttack = false;
        currentFeatherIndex++;
        if(currentFeatherIndex >= fthrs.Length)
            currentFeatherIndex = 0;
    }*/
}