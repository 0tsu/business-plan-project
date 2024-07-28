using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ainda vai ser estruturado mais por conta de ser pequeno detalhe vou deixar pra depois porem a grosso modo
/// sera um behavior que sera responsavel em verificar quanto tempo o GameObject vai esta na animação de pulo
/// e quando ele passar de um certo tempo na animação de pulo quando ele entrar em contato com o chão ele vai fazer uma animação diferente
/// </summary>
public class HighPlaceFallBehavior : StateMachineBehaviour
{
    [SerializeField] string triggerName;
    [SerializeField] float time;
    float currentTime;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(currentTime >= time)
        {
            animator.SetTrigger(triggerName);
        }
        currentTime += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime = 0;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
