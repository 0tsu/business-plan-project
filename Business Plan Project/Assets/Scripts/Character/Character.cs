using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    
    [Header("Attack variables")]
    public bool isAttacking;
    protected Rigidbody2D rb2D;
}
