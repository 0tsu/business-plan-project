using UnityEngine;

public class CurvaDeBezier : MonoBehaviour
{
    public Transform pontoInicial; // Ponto inicial da curva
    public Transform pontoControle; // Ponto de controle da curva
    public Transform pontoFinal; // Ponto final da curva
    public float duracao = 5.0f; // Duração da animação da curva em segundos

    private float tempoDecorrido = 0.0f;

    private void Update()
    {
        // Defina um valor de t entre 0 e 1 (representando o progresso da curva)
        float t = 0.5f; // Por exemplo, um valor de t = 0.5 estaria no meio da curva

        // Calcule a posição na curva de Bezier para o valor de t
        Vector3 posicaoCurva = GetPosicaoCurva(pontoInicial.position, pontoControle.position, pontoFinal.position, t);

        // Mova o objeto para a posição da curva
        transform.position = posicaoCurva;
    }

    // Função que calcula a posição da curva de Bezier
    private Vector3 GetPosicaoCurva(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 posicaoCurva = uu * p0 + 2 * u * t * p1 + tt * p2;
        return posicaoCurva;
    }
}
