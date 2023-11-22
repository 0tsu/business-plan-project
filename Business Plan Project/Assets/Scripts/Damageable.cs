using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class Damageable : MonoBehaviour
{


    Animator animator;
    Rigidbody2D rb;

    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    public bool canMove { get { return animator.GetBool(AnimationString.CanMove); } }
    public bool isHit { get { return animator.GetBool(AnimationString.IsHit); } set { animator.SetBool(AnimationString.IsHit, value); } }
    public bool isAlive { get { return animator.GetBool(AnimationString.IsAlive); } }

    bool isInvulnerability;
    
    [SerializeField] float invulnerabilityTime;
    float currentInvulnerabilityTime = 0;
    [SerializeField] Vector2 knockBack;

    

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        currentHealth = maxHealth;    
        animator = GetComponent<Animator>();
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

    public void Hit(int damege, Damageable hitObj)
    {
        if (!isInvulnerability && isAlive)
        {
            currentHealth -= damege;
            animator.SetTrigger(AnimationString.HitTrigger);
            IHit hit = hitObj.GetComponent<IHit>();
            hit.OnHit(knockBack);
            isInvulnerability = true;
        }
    }
}
