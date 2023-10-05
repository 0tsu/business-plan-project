using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour, IAttack
{
    [SerializeField] Transform player;

    [Header ("Move variables")]
    [SerializeField] float speed = 1f;
    [SerializeField] public float speedAttack = 0.5f;
    [SerializeField] float smoothness = 0.3f;

    [Header("Spacing Variables")]
    [SerializeField] float spacingX;
    [SerializeField] float spacingY;

    //private bool onFAttack;
    Vector3 targetPosition;
    public float SpacingX{
        get { return spacingX; }
        set { spacingX = value; }
    }
    public float SpacingY{
        get { return spacingY; }
        set { spacingY = value; }
    }

    Vector3 currentVelocity;
    
    [Header ("Rotate variables")]
    [SerializeField] float rotationSpeed = 10f;
    
    [Header ("Sine variables")]
    [SerializeField] float frequency = 0.4f;
    [SerializeField] float amplitude = 0.1f;
    
    

    void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        player = playerGameObject.GetComponent<Transform>();
    }
    void Update(){
        MoveFeather();
        RotationFeather();
        Attack();
    }

    void MoveFeather(){ 
        float yOffset = Mathf.Sin(Time.time * speed * frequency) * amplitude;
        Vector3 targetPosition = player.position + new Vector3(spacingX * player.localScale.x, yOffset + spacingY, 0f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
    }
    void RotationFeather()
    {
        Vector3 targetDirection = player.position - transform.position; 
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward); 
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    public void Attack()
    {
        targetPosition = player.position + new Vector3(1f, 0f,0f);
        if (Input.GetKey(KeyCode.B))
            StartCoroutine(AttackTime());
    }
    public IEnumerator AttackTime()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, speedAttack * Time.deltaTime);
        Quaternion targetRotation = Quaternion.AngleAxis(0, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        yield return null;
    }

}
