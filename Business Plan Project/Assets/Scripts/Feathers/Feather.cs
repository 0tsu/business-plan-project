using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{
    [SerializeField] Transform player;

    [Header ("Move variables")]
    [SerializeField] float speed = 1f;
    [SerializeField] float smoothness = 0.3f;

    [Header("Spacing Variables")]
    [SerializeField] float spacingX;
    [SerializeField] float spacingY;
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
    }

    void MoveFeather(){ 
        float yOffset = Mathf.Sin(Time.time * speed * frequency) * amplitude;//responsavel de fazer um movimento usando o seno
        Vector3 targetPosition = player.position + new Vector3(spacingX * player.localScale.x, yOffset + spacingY, 0f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);//
    }
    void RotationFeather()
    {
        Vector3 targetDirection = player.position - transform.position;//verifica a direção/distancia das penas para o player
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;//Will assign the result of the tangent in relation to the distance of the feather and the player
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);//Will rotate the feather along the Z axis
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);//will perform the rotation from freme to freme     
    }
    
}
