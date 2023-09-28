using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationCharacter : MonoBehaviour
{
    protected Animator animator;
    protected string currentState;
    protected const string idle = "Idle"; 
    protected const string walk = "Walk"; 
    protected const string attack = "Attack";
    protected virtual void AnimationState(string stateControl)
    {
        if (currentState == stateControl) return; 
        animator.Play(stateControl);
        currentState = stateControl;
    }
}
