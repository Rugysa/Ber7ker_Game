using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Player_Attak : MonoBehaviour
{
    [Header("stats")]
    [SerializeField] private int power;
    [Header("animation")]

    [SerializeField]private Animator animator;

    private float Timer = 0.0f;
    private float timeToAttack = 0.25f;
    [SerializeField] private int damage = 5;

    [SerializeField] private LayerMask layerPlayer;
    private bool attacking = false;
    Vector2 ecart = new Vector2(0.1f, 0.0f);
    Vector2 size_attack_area = new Vector2(0.36f,0.45f);

    private bool estTouche = false;
    private float TimerD =0f;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
       
    	if (Input.GetButtonDown("Attak") &&!attacking)
        //if (Input.GetKeyDown(KeyCode.R) &&!attacking)
        {
            animator.SetBool("attack", true);

            if(this.transform.localScale.x < 0) {
                ecart = new Vector2(0.2f, 0);
                Collider2D[] list_ennemy = Physics2D.OverlapBoxAll((Vector2)this.transform.position - ecart, size_attack_area, 0f, layerPlayer);
                Attack(list_ennemy);
            }
            else {
                Collider2D[] list_ennemy = Physics2D.OverlapBoxAll((Vector2)this.transform.position + ecart, size_attack_area, 0f, layerPlayer);
                Attack(list_ennemy);
            }


        }

        if (attacking)
        {
            Timer += Time.deltaTime;

            if(Timer > timeToAttack)
            {
                animator.SetBool("attack", false);
                Timer = 0f;
                attacking = false;
            }
        }

        if(estTouche)
        {
            TimerD += Time.deltaTime;
            if(TimerD > 3f)
            {

            }
        }
    }

    private void Attack(Collider2D[] list_ennemy) {
        attacking = true;

        foreach(Collider2D col in list_ennemy)
        {

            if (col.gameObject.CompareTag("Ennemie"))
            {
                if(col.GetComponent<Ennemi_Patrol_Bas>()!= null)
                {
                    col.GetComponent<Ennemi_Patrol_Bas>().estTouche = true;
                }
                else
                {
                    col.GetComponent<EnnemiDist>().estTouche = true;
                }      
                col.GetComponent<Health>().Damage(damage);
                //Pousser les ennemis
                if (this.gameObject.transform.localScale.x > 0)
                {
                    col.gameObject.transform.position = new Vector2(col.gameObject.transform.position.x + 0.1f, col.gameObject.transform.position.y);
                }
                if (gameObject.transform.localScale.x < 0)
                {
                    col.gameObject.transform.position = new Vector2(col.gameObject.transform.position.x - 0.1f, col.gameObject.transform.position.y);
                }
            }
        }
    }
}
