using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpableFeatherSpot : MonoBehaviour
{
    public UnityEvent<Vector2,bool> OnJumpebleFeather;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControl player = other.GetComponent<PlayerControl>();
        if (player == null) return;
        
        OnJumpebleFeather?.Invoke(transform.position, true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerControl player = other.GetComponent<PlayerControl>();
        if (player == null) return;
        Debug.Log("saiu Colidiu");
        OnJumpebleFeather?.Invoke(transform.position, false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
