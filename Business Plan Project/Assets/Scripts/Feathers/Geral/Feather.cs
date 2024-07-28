using Assets.Scripts.Feathers;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Feather : MonoBehaviour
{
    protected PlayerControl player;
    protected Collider2D collider2d;

    [Header("Move variables")]
    [SerializeField] float speed = 1f;
    [SerializeField] float smoothness = 0.3f;

    protected Vector3 targetPosition;
    protected Quaternion targetRotation;

    public float spacingY { get; set; }
    public float spacingX { get; set; }

    protected Vector3 currentVelocity;

    [Header("Rotate variables")]
    [SerializeField] protected float rotationSpeed = 10f;

    [Header("Sine variables")]
    [SerializeField] float frequency = 0.4f;
    [SerializeField] float amplitude = 0.1f;

    protected virtual void Start(){
        player = FindAnyObjectByType<PlayerControl>();
        collider2d = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        OnDead();
        RotationFeather();
        MoveFeather();
    }
    protected void MoveFeather(){
        float yOffset = Mathf.Sin(Time.time * speed * frequency) * amplitude;
        Vector3 targetPosition = player.transform.position + new Vector3(spacingX * player.transform.localScale.x, yOffset + spacingY, 0f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
    }
    protected void RotationFeather(){
        Vector3 targetDirection = player.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    protected void OnDead()
    {
        //Seria melhor chamar essa função por meio de um evento 
        //Para quando o player morre chamar um evento paras penas serem destruidas  
        if (!player.isAlive)
            Destroy(gameObject,0.2f);
    }
}
