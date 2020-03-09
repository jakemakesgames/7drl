using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    Rigidbody2D rb2d;

    public float lifeTime;

    public int damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", lifeTime);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damage);
        }
        
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    
}
