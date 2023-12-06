using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{


    [SerializeField]Animator animator;
    Rigidbody2D rb;

    public UnityEvent<Vector2> OnHit;

    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    public bool canMove { get { return animator.GetBool(AnimationString.CanMove); } }
    public bool isHit { get { return animator.GetBool(AnimationString.IsHit); } 
                private set { animator.SetBool(AnimationString.IsHit, value); } }
    public bool isAlive { get { return animator.GetBool(AnimationString.IsAlive); } }

    bool isInvulnerability;
    
    [SerializeField] float invulnerabilityTime;
    float currentInvulnerabilityTime = 0;
    

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        currentHealth = maxHealth;    
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        OnInvulnerable();
        IsDead();
    }

    void IsDead()
    {
        if (currentHealth <= 0 && isAlive)
        {
            animator.SetBool(AnimationString.IsAlive, false);
        }
    }

    void OnInvulnerable()
    {
        if (isInvulnerability) {
            if(currentInvulnerabilityTime >= invulnerabilityTime)
            {
                isInvulnerability = false;
                currentInvulnerabilityTime = 0f;
            }
            currentInvulnerabilityTime += Time.deltaTime;
        }

    }

    public void Hit(int damege, Vector2 knockback)
    {
        if (!isInvulnerability && isAlive)
        {
            currentHealth -= damege;
            animator.SetTrigger(AnimationString.HitTrigger);
            OnHit?.Invoke(knockback);
            isInvulnerability = true;
        }
    }
}
