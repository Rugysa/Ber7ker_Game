using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eclair : MonoBehaviour
{
    public float timeToDeath = 1f;
    [SerializeField] int damage = 3;

    [SerializeField] private LayerMask layerPlayer;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
      //  animator = GetComponent<Animator>();
       // animator.SetBool("lance", true);
    }


    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeToDeath);
       // animator.SetBool("lance", false);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (collision.GetComponent<Health>() != null)
            {
                Health health = collision.GetComponent<Health>();
                health.Damage(damage);
                CameraShaker.Instance.Shake(2.0f, 1.0f);
            }
        }
    }
}
