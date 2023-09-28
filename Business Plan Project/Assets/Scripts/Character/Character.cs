using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    
    [Header("Attack variables")]
    [SerializeField] protected bool onPAttack;
    public bool isAttacking { get; set; }
    [SerializeField] protected float attackTime;
    [SerializeField] protected float cooldownAttack;
    protected Rigidbody2D rb2D;
}
