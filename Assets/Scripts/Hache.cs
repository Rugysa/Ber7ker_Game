using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hache : MonoBehaviour
{
    public float timeToDeath = 1f;
    [SerializeField] int damage = 3;

    [SerializeField] private LayerMask layerPlayer;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
        animator = GetComponent<Animator>();
        animator.SetBool("lance", true);
    }


    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeToDeath);
        animator.SetBool("lance", false);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemie"))
        {
            if (collision.GetComponent<Ennemi_Patrol_Bas>() != null)
            {
                collision.GetComponent<Ennemi_Patrol_Bas>().estTouche = true;
            }
            else
            {
                collision.GetComponent<EnnemiDist>().estTouche = true;
            }

            if (collision.GetComponent<Health>() != null)
            {
                Health health = collision.GetComponent<Health>();
                health.Damage(damage);
            }
        }

    }


}
