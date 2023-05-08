using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script responsavel por controlar as animações no jogador

public class Animation : MonoBehaviour
{
    Animator _animator; //Cria uma variavel da classe Animator
    MovePlayer _player; //Cria uma variavel da classe MovePlayer

    string currentState; //Cria uma variavel do tipo de texto responsavel pelo controle de estados do personagem
    const string _idle = "PlayerIdle"; //variavel constante do tipo texto que define a animação do player ficar parado
    const string _walk = "PlayerRun"; //variavel constante do tipo texto que define a animação do player andar
    const string _attack = "PlayerAttack";

    void Start()
    {
        _animator = GetComponent<Animator>(); //Pega os componentes do "Animator"
        _player = FindAnyObjectByType<MovePlayer>(); //Procura e pega os componentes de um objeto que contenha o "MovePlayer" que no caso é o proprio player
    }

    // Update is called once per frame
    void Update() //Executa a todo momento os metodos e funções que estejam na chaves do "Update"
    {
        MoveAnimation(); //Metodo responsavel em definir as animações de movimentação do player
        AttackAnimation();
    }

    void MoveAnimation()
    {
        if (!_player._attack)
        {
            //Se o player estiver em movimento ele vai executar a animação de andar
            if (_player.xAxis != 0)
            {
                AnimationState(_walk);
            }
            //Se não o player vai executar a animação de quando ele estiver parado
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

    void AnimationState(string stateControl) //Metodo responsavel para controlar a animação 
    {
        if (currentState == stateControl) return; //Caso ele esteja executando a mesma animação ele não vai executar os codigos a baixo
        
        // Altera a animação do personagem para a animação especificada e atualiza o estado atual da animação.
        _animator.Play(stateControl); 
        currentState = stateControl; 
    }
}
