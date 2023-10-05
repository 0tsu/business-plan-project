using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherControl : MonoBehaviour
{
    Feather fthr;
    [SerializeField]GameObject[] featherPrefab;
    [SerializeField] float spacingX;
    [SerializeField] float spacingY;
    [SerializeField] float speedAttack;
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        
        for (int i=0; i< featherPrefab.Length; i++){
            GameObject spawnFeather = Instantiate(featherPrefab[i],player.position, Quaternion.identity);
            fthr = spawnFeather.GetComponent<Feather>();
            
            spacingX += i > featherPrefab.Length / 2 ? 0.1f : 0f;  
            spacingY = 0.1f*i;
            fthr.speedAttack = speedAttack;
            fthr.SpacingX = spacingX;
            fthr.SpacingY = spacingY;
        }
    }
    
}
