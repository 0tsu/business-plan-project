using UnityEngine;
using UnityEngine.UIElements;

public class Feathers : MonoBehaviour
{
    [SerializeField] Transform player;

    [Header ("Sine variables")]
    [SerializeField] float frequency;
    [SerializeField] float amplitude;

    [Header ("Move and Rotate variables")]
    [SerializeField] bool flip;
    
    [SerializeField] float smoothness;
    [SerializeField]float rotationSpeed;

    Vector3 currentVelocity;

    [SerializeField] float spacingX;
    [SerializeField] float spacingY;

    [SerializeField] float speed;
    public bool onAttackFeather;
    [SerializeField] float offSetAttackX;

    [SerializeField] TrailRenderer trainRenderer;

    private void Start()
    {
        trainRenderer = GetComponentInChildren<TrailRenderer>();
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject == null)
        {
            Debug.LogWarning("Player is not on scene");
            return;
        }
        player = playerGameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        Flip();
        if(onAttackFeather)
        {
            AttackFeather();
            return;
        }
        MoveFeather();
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        if (player.position.x > transform.position.x)
        {
            scale.y = Mathf.Abs(scale.y) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.y = Mathf.Abs(scale.y) * (flip ? -1 : 1);
        }
        transform.localScale = scale;

    }

    
    private void MoveFeather()
    {
        if (player == null || onAttackFeather)
        {
            return; // Verifica se há um jogador definido
        }
        float moveY = Mathf.Sin(Time.time * frequency) * amplitude;
        float moveX = Mathf.Cos(Time.timeSinceLevelLoad) * amplitude * 0;
        
        Vector3 targetDirection = player.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float offsetY = spacingY;
        float offsetX = spacingX * player.localScale.x; // index é o índice do objeto
        Vector3 targetPosition = player.position + new Vector3(moveX + offsetX, moveY + offsetY, 0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition,ref currentVelocity, smoothness);
    }
    public void AttackFeather()
    {
        float angle = player.localScale.x <= 0f ? 180f : 0f;
        Quaternion point = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, point , 10 * Time.deltaTime);

        Vector3 targetPosition = player.position + new Vector3(offSetAttackX * player.localScale.x, 0f, 0f);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    public void ToggleTrainRender(bool stateTrain)
    {
            trainRenderer.enabled = stateTrain;
            if(!stateTrain)
            {
                trainRenderer.Clear();
            }
    }

}
