using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    //Variables
    [SerializeField]//Для того что бы переменная отображалась в "инспекторе"
    private float speed = 3.0F;
    [SerializeField]
    private int lives = 5;
    [SerializeField]
    private float jumpForce = 15.0F;
    private bool isGrounded = false;

    //Components
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //Initialize
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    //Метод лучше использоваьб для физики
    private void FixedUpdate()
    {
        CheckGround();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();

        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();
    }

    //Run
    private void Run()
    {
        //Time.deltaTime - время между текущим и предыдущим фреймом

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        spriteRenderer.flipX = direction.x < 0;
    }

    //Jump
    private void Jump()
    {
        //применяет силу к твердому телу
        rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse/*(тип силы)*/);
    }

    //Check ig character grounded
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGrounded = colliders.Length > 1;
    }

}
