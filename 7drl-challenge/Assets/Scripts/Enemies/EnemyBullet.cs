using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", lifeTime);

        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // damage the player
            PlayerHealthController.instance.DamagePlayer();
        }

        Destroy(gameObject);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
