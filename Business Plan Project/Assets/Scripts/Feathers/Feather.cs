using UnityEngine;

public class Feather : MonoBehaviour, IMove, IAttack
{
    [SerializeField] PlayerControl player;

    [Header("Move variables")]
    [SerializeField] float speed = 1f;
    [SerializeField] float smoothness = 0.3f;

    public float spacingY { get; set; }
    public float spacingX { get; set; }

    public float speedAttack { get; set; }

    float attackPosition = 1f;

    Vector3 currentVelocity;

    [Header("Rotate variables")]
    [SerializeField] float rotationSpeed = 10f;

    [Header("Sine variables")]
    [SerializeField] float frequency = 0.4f;
    [SerializeField] float amplitude = 0.1f;
    public bool isAttacking { get; set; }


    void Start()
    {
        player = FindAnyObjectByType<PlayerControl>();
    }
    void Update()
    {
        if (isAttacking)
        {
            Attack();
            return;
        }
        Move();
        RotationFeather();
    }

    public void Move()
    {
        float yOffset = Mathf.Sin(Time.time * speed * frequency) * amplitude;
        Vector3 targetPosition = player.transform.position + new Vector3(spacingX * player.transform.localScale.x, yOffset + spacingY, 0f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
    }
    void RotationFeather()
    {
        Vector3 targetDirection = player.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    public void Attack()
    {
        float angle = player.transform.localScale.x <= 0f ? 180f : 0f;

        Vector3 targetPosition = player.transform.position + new Vector3(attackPosition * player.transform.localScale.x, 0f, 0f);
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedAttack * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
