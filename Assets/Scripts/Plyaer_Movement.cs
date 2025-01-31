using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    float movementX = 0;

    [SerializeField] // [] pour donner instru à unity
    float speed = 2;

    [SerializeField]
    float jumpForce = 7;

    private float Seuil_arret= 0.1f;

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private bool wantsToJump;
    [SerializeField] private bool canJump = false;
    [SerializeField] private float gravite = 1.0f;
    [SerializeField] private bool second_jump = false;
    [SerializeField] private bool want_second_jump = false;

 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");


        if (canJump)
        {
            if (Input.GetButtonDown("Jump")) 
            //if (Input.GetKeyDown(KeyCode.F))
            {
                wantsToJump = true;
  
            }
        }

        if (second_jump)
        {   if (Input.GetButtonDown("Jump")) 
            //if (Input.GetKeyDown(KeyCode.F))
            {
                want_second_jump = true;
            }
        }

        animator.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));     
        
        if(gameObject.transform.position.y < -2.5f)
        {
            gameObject.GetComponent<Health>().Die();
        }
    }

    private void FixedUpdate()
    {
        if(wantsToJump && canJump || want_second_jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        rb.velocity += new Vector2(movementX * speed, 0);
        rb.velocity = new Vector2(Mathf.Clamp(movementX * speed, -speed, speed),rb.velocity.y);
        
        if (wantsToJump && canJump){
 
            wantsToJump = false;
            canJump = false;
            second_jump = true;
        }
        if(second_jump && want_second_jump)
        {
           // StartCoroutine(Attente)
            second_jump = false;
            want_second_jump = false;
        }
        if (Mathf.Abs(rb.velocity.x) < Seuil_arret)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if(Mathf.Abs(rb.velocity.y) < Seuil_arret)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (rb.velocity.magnitude >0 && canJump)
        {
            animator.SetBool("canMove", true);
        }
        else {
            animator.SetBool("canMove", false);
        }

        if (!canJump)
        {
            rb.velocity += new Vector2(0, -gravite*Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.enabled)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                canJump = true;
                second_jump = false;
                //rb.velocity = new Vector2(rb.velocity.x, 0);
            }

        }
    }
}
