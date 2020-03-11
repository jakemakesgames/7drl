using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector3 moveDirection;

    public float deceleration = 5f;
    public float lifeTime = 3f;

    public SpriteRenderer spriteRenderer;
    public float fadeSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, Mathf.MoveTowards(spriteRenderer.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (spriteRenderer.color.a == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
