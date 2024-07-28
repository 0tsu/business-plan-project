using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Rainbow : MonoBehaviour
{
    public float duration = 5.0f; // Tempo para completar um ciclo de arco-íris
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Light2D Objectlight;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Objectlight = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time / duration, 1);

        Color rainbowColor = Color.HSVToRGB(t, 1, 1);
        if(spriteRenderer != null)
            spriteRenderer.color = rainbowColor;
        if(Objectlight != null)
            Objectlight.color = rainbowColor;
    }
}
