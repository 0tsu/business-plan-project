using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Feathers : MonoBehaviour
{
    [SerializeField] Transform featherPivot;
    [SerializeField] Transform player;

    [SerializeField] float frequency;
    [SerializeField] float amplitude;

    [SerializeField] float smoothness;

    [SerializeField]float rotationSpeed;

    [SerializeField] bool flip;
    Vector3 currentVelocity;

    [SerializeField] int index;

    [SerializeField] float spacingX;
    [SerializeField] float spacingY;

    private void Start()
    {
        // Definir o valor de index para cada objeto
        Feathers[] featherScripts = FindObjectsOfType<Feathers>();
        for (int i = 0; i < featherScripts.Length; i++)
        {
            featherScripts[i].index = i;
        }
    }

    private void Update()
    {
        Flip();
        MoveFeathers();
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        if (player.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * 1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        transform.localScale = scale;

    }
    private void MoveFeathers()
    {
        if (player == null)
        {
            return; // Verifica se há um jogador definido
        }

        float moveY = Mathf.Sin(Time.time * frequency) * amplitude;
        float moveX = Mathf.Cos(Time.timeSinceLevelLoad) * amplitude;
        //Vector3 targetPosition = featherPivot.position + new Vector3(moveX, moveY, 0f); // Posição alvo é a posição do jogador
        
        Vector3 targetDirection = player.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float offsetY = index * spacingY; // index é o índice do objeto
        float offsetX = index * spacingX * player.localScale.x; // index é o índice do objeto
        Vector3 targetPosition = featherPivot.position + new Vector3(moveX + offsetX, moveY + offsetY, 0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition,ref currentVelocity, smoothness);
    }
}
