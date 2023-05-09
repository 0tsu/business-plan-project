using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float bulletForce;
    [SerializeField] MovePlayer _player;
    [SerializeField] float destroyBullet;
    // Update is called once per frame
    private void Start()
    {
        _player = FindAnyObjectByType<MovePlayer>();
        GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletForce * _player.side,transform.position.y), ForceMode2D.Impulse);
        Destroy(gameObject, destroyBullet);
    }
    /*void Update()
    {
        transform.Translate(transform.right * bulletForce * Time.deltaTime);
    }*/
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }*/
}
