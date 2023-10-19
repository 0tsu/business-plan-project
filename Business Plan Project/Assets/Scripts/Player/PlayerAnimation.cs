using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAnimation : AnimationCharacter//, IAnimationMove, IAnimationAttack, IAnimationIdle
{
    //PlayerControl player;

    //private bool isIdleAnimation = false;
    //private float idleTimer;
    //const string veryWait = "veryWait";

    //void Start()
    //{
    //    animator = GetComponent<Animator>(); 
    //    player = FindAnyObjectByType<PlayerControl>();
    //}
    //void Update() 
    //{
    //    MoveAnimation();
    //    AttackAnimation();
    //}
    //public void IdleAnimation()
    //{
    //    if (!isIdleAnimation)
    //    {
    //        AnimationState(idle);
    //        isIdleAnimation = true;
    //        StartCoroutine(WaitingLot());
    //    }
    //}
    //public void MoveAnimation()
    //{
    //    if (!player.isAttacking)
    //    {
    //        if(player.xAxis != 0f)
    //        {
    //            isIdleAnimation = false;
    //            AnimationState(walk);
    //        }
    //        else
    //        {
    //            IdleAnimation();
    //            VeryWaitingLot();
    //        }
    //    }
        
    //}
    //void VeryWaitingLot()
    //{
    //    if (isIdleAnimation)
    //    {
    //        idleTimer += Time.deltaTime;

    //        if (idleTimer >= 5f)
    //        {
    //            idleTimer = 0f;
    //            isIdleAnimation = false;
    //            AnimationState(veryWait);
    //        }
    //    }
    //}
    //IEnumerator WaitingLot()
    //{
    //    bool Time = true;
    //    yield return new WaitForSeconds(3f);
    //    if(!Time)
    //    {
    //        yield return new WaitForSeconds(5f);
    //        Time = true;
    //    }
    //    else if (isIdleAnimation && player.xAxis == 0f)
    //    {
    //        AnimationState(veryWait);
    //        Time = false;
    //    }

    //}
    //public void AttackAnimation()
    //{   
    //    if (player.isAttacking)
    //    {
    //        AnimationState(attack);
    //    }
    //}
}
