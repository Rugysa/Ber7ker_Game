using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ennemi_Patrol_Bas : MonoBehaviour
{
    float movementX = 0;

    public float speed;
    private Transform[] waypoints;
    public SpriteRenderer graphics;

    private Transform target;
    private Rigidbody2D rb;
    private Animator animator;
    private int destPoint = 1;

    private float Timer = 0.0f;
    private float timeToAttack = 0.25f;
    [SerializeField] private int damage = 5;

    [SerializeField] private LayerMask layerPlayer;
    private bool attacking = false;
    Vector2 ecart = new Vector2(0.1f, 0.0f);
    Vector2 size_attack_area = new Vector2(0.43f, 0.35f);

    public bool estTouche = false;
    private float TimerD;


    private CompteRebours compteRebours;

    private void Awake()
    {
        waypoints = new Transform[2];
        compteRebours = new CompteRebours();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (estTouche)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red; //proche du rouge
            TimerD += Time.deltaTime;
            if (TimerD > 0.2f)
            {
                TimerD = 0f;
                estTouche = false;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        movementX = Input.GetAxisRaw("Horizontal");
          Vector2 dir = target.position - transform.position;
        if (dir.x < 0)
        {
            movementX = -1;
        }
        else
        {
            movementX = 1;
        }

        // Si l'ennemi est quasiment arrivé à destination
        if (Vector2.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }
    private void FixedUpdate()
    {
        if (!attacking)
        {
            rb.velocity += new Vector2(movementX * speed, 0);
            rb.velocity = new Vector2(Mathf.Clamp(movementX * speed, -speed, speed), rb.velocity.y);
        }

        if (rb.velocity.magnitude > 0)
        {
            animator.SetBool("canMove", true);
        }
        else
        {
            animator.SetBool("canMove", false);
        }

        if (attacking)
        {
            Timer += Time.deltaTime;
            rb.velocity = Vector2.zero;

            if (Timer > timeToAttack)
            {
                animator.SetBool("attack", false);
                Timer = 0f;
                StartCoroutine(AttenteFinAttack());
                attacking = false;
            }
        }
    }

    public void SetwayPoints (Transform Point1, Transform Point2)
    {
        waypoints[0] = Point1;
        waypoints[1] = Point2;
        target = waypoints[1];
    }

    public void SetCompteRebours(CompteRebours cr)
    {
        compteRebours = cr;
    }

    private void Attack(Collider2D[] list_ennemy)
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        foreach (Collider2D col in list_ennemy)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.GetComponent<Health>().Damage(damage);
                CameraShaker.Instance.Shake(2.0f, 1.0f);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {;
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attack", true);

             if (graphics.flipX)
             {
                 ecart = new Vector2(0.2f, 0);
                 Collider2D[] list_ennemy = Physics2D.OverlapBoxAll((Vector2)this.transform.position + ecart, size_attack_area, 0f, layerPlayer);
                 Attack(list_ennemy);
             }
             else
             {
                 Collider2D[] list_ennemy = Physics2D.OverlapBoxAll((Vector2)this.transform.position - ecart, size_attack_area, 0f, layerPlayer);
                 Attack(list_ennemy);
             }

        }
    }

    IEnumerator AttenteFinAttack()
    {
        yield return new WaitForSeconds(100.0f);
    }
}