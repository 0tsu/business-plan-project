using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    
    [Header("Attack variables")]
    [SerializeField] protected float attackTime;
    public bool isAttacking {  get; protected set; }
    protected Rigidbody2D rb2D;
}
