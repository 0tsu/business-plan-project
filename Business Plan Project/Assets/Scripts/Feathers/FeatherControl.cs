using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherControl : MonoBehaviour
{
    [SerializeField]GameObject[] featherPrefab;
    [SerializeField] float spacingX;
    [SerializeField] float spacingY;
    [SerializeField] float speedAttack;
    

    void Start()
    {

        PlayerControl player = FindObjectOfType<PlayerControl>();
        Transform playerPosition = player.transform;
        
        for (int i=0; i< featherPrefab.Length; i++){
            GameObject spawnFeather = Instantiate(featherPrefab[i],playerPosition.position, Quaternion.identity);
            Feather fthr = spawnFeather.GetComponent<Feather>();
            spacingX += i > featherPrefab.Length / 2 ? 0.1f : 0f;  
            spacingY = 0.1f*i;
            fthr.spacingX = spacingX;
            fthr.spacingY = spacingY;
        }
    }
}
