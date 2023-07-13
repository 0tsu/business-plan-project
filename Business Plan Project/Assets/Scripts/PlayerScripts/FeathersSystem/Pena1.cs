using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pena1 : MonoBehaviour
{
    [SerializeField] Transform player; // Referência ao transform do jogador
    [SerializeField] float distMinima; // Distância mínima para parar de seguir o jogador
    [SerializeField] float velocidade; // Velocidade de movimento do objeto
    [SerializeField] float suavidade;
    private Vector2 velocidadeAtual;
    bool flip;

    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Flip();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (player != null)
        {
            // Calcula a direção para o jogador
            Vector2 direcao = (player.position - transform.position).normalized;

            // Calcula a distância para o jogador
            float distancia = Vector2.Distance(transform.position, player.position);

            if (distancia > distMinima)
            {
                // Ajusta a velocidade gradualmente usando lerp
                velocidadeAtual = Vector2.Lerp(velocidadeAtual, direcao * velocidade, suavidade * Time.deltaTime);

            }
            else
            {
                // Reduz a velocidade gradualmente até parar
                velocidadeAtual = Vector2.Lerp(velocidadeAtual, Vector2.zero, suavidade * Time.deltaTime);
            }
            // Aplica a velocidade ao Rigidbody
            rb2D.velocity = velocidadeAtual;
        }
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
}
