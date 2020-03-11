using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    private Camera cam;
    public Animator anim;

    public SpriteRenderer bodyRenderer;

    // movement
    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D rb2d;

    // dashing
    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = .5f;
    public float dashCooldown = 1f;

    [HideInInspector] public float dashCounter;
    private float dashCoolCounter;

    public float dashInvincibility = .5f;

    // shooting
    public Transform gunArm;

    public GameObject bullet;
    public Transform firePoint;
    public float timeBtwnShots;
    private float shotCounter;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

        rb2d.velocity = moveInput * activeMoveSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

        // is the mouse to the left of the player?
        if (mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3 (-1, 1, 1);
            gunArm.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }


        // rotate gun arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);

        // shoot
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            shotCounter = timeBtwnShots;
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                shotCounter = timeBtwnShots;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                // dash
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;

                anim.SetTrigger("dash");

                PlayerHealthController.instance.MakeInvincible(dashInvincibility);
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

    }
}
