using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingController : MonoBehaviour
{
    [SerializeField] Transform GroundCollider;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] Transform WallCollider;
    [SerializeField] LayerMask WallLayer;

    public bool IsGround()
    {
        if(GroundCollider != null)
            return Physics2D.OverlapBox(GroundCollider.position, GroundCollider.localScale, 0f, GroundLayer);
        return true;
    }
    public bool IsWalled()
    {
        if(WallCollider != null)
            return Physics2D.OverlapBox(WallCollider.position, WallCollider.localScale, 0f, WallLayer);
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(GroundCollider != null)
            Gizmos.DrawWireCube(GroundCollider.position, GroundCollider.localScale);
        if(WallCollider != null)
            Gizmos.DrawWireCube(WallCollider.position, WallCollider.localScale);
    }
}
