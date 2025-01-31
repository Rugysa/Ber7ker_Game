using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LancerEclair : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Transform RightSpawn;
    [SerializeField] GameObject Eclair;
    private Animator animator;
    [SerializeField] private float seuil = 0.1f;
    public SpriteRenderer graphics;

    public float range = 1f;
    public float speedLancer = 1.5f;
    [SerializeField] private float elcairRate = 0.001f;
    public bool canLancer = true;

    private float Timer=0f;
    private float tps_reinit = 3f;
    private bool aGauche;

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public void Lancer_anim()
    {
        if (!aGauche)
        {
            animator.SetBool("attack", true);
            Lancer(RightSpawn, new Vector2(1, 0));
        }
        else if(aGauche){
            animator.SetBool("attack", true);
            Lancer(RightSpawn, new Vector2(-1, 0));
        }
    }


    public void Lancer(Transform spawnPoint, Vector2 lancerDirection)
    {
        animator.SetBool("attack", false);
        GameObject eclair = Instantiate(Eclair, spawnPoint.position, Quaternion.identity);
        eclair.GetComponent<Rigidbody2D>().velocity = lancerDirection * speedLancer;
        eclair.GetComponent<Eclair>().timeToDeath = range;
        if (lancerDirection.x < 0)
        {
            eclair.transform.localScale = new Vector3(-eclair.transform.localScale.x, eclair.transform.localScale.y, eclair.transform.localScale.z);
        }
        StartCoroutine(EclairRate());
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

        private void FixedUpdate()
    {

       /* if (Input.GetKeyDown(KeyCode.J) && canLancer) { 
            Lancer(RightSpawn, new Vector2(-1, 0));
            StartCoroutine(EclairRate());
        }*/

        if(!canLancer)
        {
          /*  if(Timer_anim < tps_anim)
            {
                Timer_anim +=Time.Fi
            }*/

            if(Timer < tps_reinit)
            {
                Timer += Time.fixedDeltaTime;
                //Debug.Log("maj timer");
                if (Timer > tps_reinit)
                {
                    Timer = 0f;
                    canLancer = true;
                }
           
            }
        }
        
       // Debug.Log(gameObject.transform.position.y - player.transform.position.y);
        if (Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) < seuil && canLancer)
        {
           // Debug.Log(gameObject.transform.position.y - player.transform.position.y);
           // Debug.Log("dans if");
            if (gameObject.transform.position.x < player.transform.position.x)
            //le player est à droite du mob
            {
                if (graphics.flipX)
                //L'ennemei regarde dans la direction ou se trouve le joueur
                {
                    animator.SetBool("attack", true);
                    canLancer = false;
                    aGauche = false;
                   /* Lancer(RightSpawn, new Vector2(1, 0));
                    StartCoroutine(EclairRate());*/
                }
            }
            else
            // le player est à gauche du mob
            {
                if (!graphics.flipX)
                //L'ennemei regarde dans la direction ou se trouve le joueur
                {
                    animator.SetBool("attack", true);
                    canLancer = false;
                    aGauche = true;
                   // StartCoroutine(EclairRate());
                   /* Lancer(RightSpawn, new Vector2(-1, 0));
                    StartCoroutine(EclairRate());
                    animator.SetBool("attack", false);*/
                }
            }
        }
    }

    IEnumerator EclairRate()
    {
        float tps_attnte = 1 / elcairRate;
        // canLancer = true;
        yield return new WaitForSeconds(tps_attnte);
        

    }
}
