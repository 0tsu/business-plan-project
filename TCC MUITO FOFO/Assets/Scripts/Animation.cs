using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script responsavel por controlar as anima��es no jogador

public class Animation : MonoBehaviour
{
    Animator _animator; //Cria uma variavel da classe Animator
    MovePlayer _player; //Cria uma variavel da classe MovePlayer

    string currentState; //Cria uma variavel do tipo de texto responsavel pelo controle de estados do personagem
    const string _idle = "PlayerIdle"; //variavel constante do tipo texto que define a anima��o do player ficar parado
    const string _walk = "PlayerRun"; //variavel constante do tipo texto que define a anima��o do player andar
    const string _attack = "PlayerAttack";

    void Start()
    {
        _animator = GetComponent<Animator>(); //Pega os componentes do "Animator"
        _player = FindAnyObjectByType<MovePlayer>(); //Procura e pega os componentes de um objeto que contenha o "MovePlayer" que no caso � o proprio player
    }

    // Update is called once per frame
    void Update() //Executa a todo momento os metodos e fun��es que estejam na chaves do "Update"
    {
        MoveAnimation(); //Metodo responsavel em definir as anima��es de movimenta��o do player
        AttackAnimation();
    }

    void MoveAnimation()
    {
        if (!_player._attack)
        {
            //Se o player estiver em movimento ele vai executar a anima��o de andar
            if (_player.xAxis != 0)
            {
                AnimationState(_walk);
            }
            //Se n�o o player vai executar a anima��o de quando ele estiver parado
            else
            {
                AnimationState(_idle);
            }
        }
    }
    void AttackAnimation()
    {   
        if (_player._attack)
        {
            AnimationState(_attack);
        }
    }

    void AnimationState(string stateControl) //Metodo responsavel para controlar a anima��o 
    {
        if (currentState == stateControl) return; //Caso ele esteja executando a mesma anima��o ele n�o vai executar os codigos a baixo
        
        // Altera a anima��o do personagem para a anima��o especificada e atualiza o estado atual da anima��o.
        _animator.Play(stateControl); 
        currentState = stateControl; 
    }
}
