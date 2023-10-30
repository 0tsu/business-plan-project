using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Feather : MonoBehaviour
{
    public delegate void OnAttackEndedAction();
    public static event OnAttackEndedAction OnAttackEnd;
    [SerializeField] PlayerControl player;

    [Header("Move variables")]
    [SerializeField] float speed = 1f;
    [SerializeField] float smoothness = 0.3f;

    Vector3 targetPosition;
    Quaternion targetRotation;

    public float spacingY { get; set; }
    public float spacingX { get; set; }

    public float speedAttack { get; set; }

    public float attackPosition = 1f;

    Vector3 currentVelocity;

    [Header("Rotate variables")]
    [SerializeField] float rotationSpeed = 10f;

    [Header("Sine variables")]
    [SerializeField] float frequency = 0.4f;
    [SerializeField] float amplitude = 0.1f;

    [SerializeField] bool isAttacking;

    void Start(){
        player = FindAnyObjectByType<PlayerControl>();
    }
    void Update(){
        if (isAttacking)
        {
            MoveAttack();
            return;
        }
        Move();
        RotationFeather();
    }
    public void Move(){
        float yOffset = Mathf.Sin(Time.time * speed * frequency) * amplitude;
        Vector3 targetPosition = player.transform.position + new Vector3(spacingX * player.transform.localScale.x, yOffset + spacingY, 0f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
    }
    void RotationFeather(){
        Vector3 targetDirection = player.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    public void Attack(){
        isAttacking = true;

        targetPosition = player.transform.position + new Vector3(attackPosition * player.transform.localScale.x, 0f, 0f);

        float angle = player.transform.localScale.x <= 0f ? 180f : 0f;
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void MoveAttack(){
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedAttack * Time.deltaTime);
        
        if((transform.position - targetPosition).magnitude < 0.05f){
            isAttacking = false;
            StartCoroutine(IsAttackEnd());
        }
    }
    IEnumerator IsAttackEnd(){
        yield return new WaitForSeconds(0.5f);
        if (OnAttackEnd != null){
            OnAttackEnd();
        }
    }
}
