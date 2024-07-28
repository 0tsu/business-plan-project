using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeatherAttack : Feather
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            MoveAttack();
            return;
        }
    }
    void MoveAttack()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedAttack * Time.deltaTime);

        if ((transform.position - targetPosition).magnitude < 0.05f)
        {
            AttackFinish();
        }
    }
    public void Attack()
    {
        collider2d.enabled = true;
        isAttacking = true;

        targetPosition = player.transform.position + new Vector3(attackPosition * player.transform.localScale.x, 0f, 0f);

        float angle = player.transform.localScale.x <= 0f ? 180f : 0f;
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void AttackFinish()
    {
        isAttacking = false;
        OnAttackEnd();
        collider2d.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttackFinish();
    }
}
