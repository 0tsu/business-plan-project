using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Rigidbody2D Rb2D;
    [SerializeField] float speedPlayer; //Variavel flutuante responsavel em definir a velocidade do player
    [SerializeField] float speedJump; //Variavel flutuante responsavel em definir a velocidade de pulo do player
    public float xAxis { get; set; } //Variavel flutuante responsavel em definir a direção do player


    void Start() //Executa apenas uma vez quando inicia o jogo
    {
        Application.targetFrameRate = 60; //Codigo que tem como objetivo travar o jogo em 60 quadros por segundo
        QualitySettings.vSyncCount = 0; //Codigo responsavel em desativar o vSync
        Rb2D = GetComponent<Rigidbody2D>(); //Codigo responsavel em pegar os componetes da classe "Rigidbody2D"
    }

    void Update() //Executa a todo momento
    {
        PlayerMove(); //Metodo responsavel por mover o player
        FlipPlayer(); //Metodo responsavel por virar o player de acordo com a direção do mesmo
    }

    private void PlayerMove()
    {
        xAxis = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speedPlayer; //define a direção e a velociade do jogador

        Rb2D.velocity = new Vector2(xAxis * speedPlayer, Rb2D.velocity.y); //Move o player

    }
    private void FlipPlayer()
    {
        //Se o player apertar a tecla "A" ou a seta da esquerda ele vai virar o player para esquerda
        if(xAxis < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //Caso ele aperte a tecla "D" ou a seta pra direita ele vai virar o player para direita
        else if(xAxis > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
