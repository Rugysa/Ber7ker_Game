using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnnemiDist : MonoBehaviour
{
    float movementX = 0;

    public float speed;
    private Transform[] waypoints;
    public SpriteRenderer graphics;

    private Transform target;
    private Rigidbody2D rb;
    private Animator animator;
    private int destPoint = 1;


    private CompteRebours compteRebours;
    private bool canLancer;


    public bool estTouche = false;
    private float TimerD;
    [SerializeField] private Color color;

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
        canLancer = gameObject.GetComponent<LancerEclair>().canLancer;
    }

    // Update is called once per frame
    void Update()
    {
        if (estTouche)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red; //proche du rouge
            TimerD += Time.deltaTime;
            if (TimerD > 0.3f)
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
        canLancer = gameObject.GetComponent<LancerEclair>().canLancer;
        if (canLancer)
        {
            rb.velocity += new Vector2(movementX * speed, 0);
            rb.velocity = new Vector2(Mathf.Clamp(movementX * speed, -speed, speed), rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (rb.velocity.magnitude > 0)
        {
            animator.SetBool("canMove", true);
        }
        else
        {
            animator.SetBool("canMove", false);
        }
       
    }

    public void SetwayPoints(Transform Point1, Transform Point2)
    {
        waypoints[0] = Point1;
        waypoints[1] = Point2;
        target = waypoints[1];
    }

    public void SetCompteRebours(CompteRebours cr)
    {
        compteRebours = cr;
    }
}
