using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherAnimation : MonoBehaviour
{
    Animator animator; //Cria uma variavel da classe Animator

    string currentState; //Cria uma variavel do tipo de texto responsavel pelo controle de estados do personagem
    
    public bool onFAttack;

    const string Idle = "IdleFeather";
    const string Attack = "AttackFeather";

    void Start()
    {
        animator = GetComponent<Animator>(); //Pega os componentes do "Animator"
    }

    void Update()
    {
        //IdleAnim();
        
    }

    private void IdleAnim()
    {
        if (!onFAttack)
        {
            Debug.Log("teste false");
            //AnimationState("IdleFeather");
        }
        else
        {
            Debug.Log("teste true");
            //AnimationState("AttackFeather");
        }
    }


    void AnimationState(string stateControl) //Metodo responsavel para controlar a animação 
    {
        if (currentState == stateControl) return; //Caso ele esteja executando a mesma animação ele não vai executar os codigos a baixo

        // Altera a animação do personagem para a animação especificada e atualiza o estado atual da animação.
        animator.Play(stateControl);
        currentState = stateControl;
    }
}
