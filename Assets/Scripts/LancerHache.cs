using System.Collections;
using UnityEngine;

public class LancerHache : MonoBehaviour
{
    [SerializeField] Transform RightSpawn;
    [SerializeField] GameObject Hache;
    private Animator animator;

    public float range = 1f;
    public float speedLancer = 1.5f;
    public float hacheRate = 2f;
    private bool canLancer = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canLancer)
        {    if (Input.GetButtonDown("Lancer")) 
           // if (Input.GetKeyUp(KeyCode.T))
            {
                animator.SetBool("Throw", true);
                if (this.transform.localScale.x >=0 ) {
                    Lancer(RightSpawn, new Vector2(1, 0));
                    StartCoroutine(HacheRate());
                }
                else {
                    Lancer(RightSpawn, new Vector2(-1, 0));
                    StartCoroutine(HacheRate());
                }
                
            }
        }
       
    }

    private void Lancer(Transform spawnPoint, Vector2 lancerDirection)
    {
        canLancer = false;
        GameObject hache = Instantiate(Hache, spawnPoint.position, Quaternion.identity);
        hache.GetComponent<Rigidbody2D>().velocity = lancerDirection * speedLancer;
        hache.GetComponent<Hache>().timeToDeath = range;
        if(lancerDirection.x < 0) 
        {
            hache.transform.localScale = new Vector3(-hache.transform.localScale.x, hache.transform.localScale.y,hache.transform.localScale.z);
        }
    }

    IEnumerator HacheRate()
    {
        float tps_attnte = 1 / hacheRate;
        yield return new WaitForSeconds(tps_attnte/2);
        animator.SetBool("Throw", false);
        yield return new WaitForSeconds(tps_attnte / 2);
        canLancer = true;
        
    }
}
