using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float delta;
    float aux;
    Vector2 direction;
    Rigidbody2D rb;

    bool isShot;
    private void Awake()
    {
        delta = 0;
        aux = 0;
        isShot = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(isShot)
        {
            aux += delta;
            rb.velocity = direction * aux;
        }
    }
    public void Shoot(Vector2 dir,float accelerationDelta)
    {
        delta = accelerationDelta;
        direction = dir;    
        isShot=true;
        Debug.Log("me lance");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("me choque");
        isShot = false;
        Collider2D collider =GetComponent<Collider2D>();
        collider.enabled = false;
        rb.velocity = Vector2.zero;
        Destroy(gameObject,GetComponentInChildren<TrailRenderer>().time);


    }


}
