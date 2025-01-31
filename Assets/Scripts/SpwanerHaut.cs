using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHaut : MonoBehaviour
{
    [SerializeField] private GameObject EnnemiCAC;
    [SerializeField] private GameObject EnnemiDist;
    [SerializeField] private GameObject player;

    [SerializeField] private Transform PointGauche;
    [SerializeField] private Transform PointDroit;
    [SerializeField] private CompteRebours compteRebours;
    [SerializeField] private Score score;
    [SerializeField] private EnnemiKill ennemiKill;

    private int spawnMax = 1;
    public int compteur = 0;

    [SerializeField] float cacInterval = 2f;

    void Start()
    {

        StartCoroutine(spawnEnnemi(cacInterval));
    }

    public void DecrementeCompteur()
    {
        compteur -= 1;
    }

    private IEnumerator spawnEnnemi(float interval)
    {
        yield return new WaitForSeconds(interval);
        if (compteur < spawnMax)
        {
            int random_spawn = Random.Range(1, 100);
            if (random_spawn < 67)
            {
                GameObject ennemi = EnnemiCAC;
                GameObject newEnnemi = Instantiate(ennemi, new Vector3(Random.Range(1.6f, 3.3f), 1.7f, 0), Quaternion.identity);
                Ennemi_Patrol_Bas ennemi_script = newEnnemi.GetComponentInChildren<Ennemi_Patrol_Bas>();
                ennemi_script.SetwayPoints(PointGauche, PointDroit);
                Health health_script = newEnnemi.GetComponentInChildren<Health>();
                health_script.SetCompteRebours(compteRebours);
                health_script.SetSpawnerHaut(this.gameObject.GetComponent<SpawnerHaut>());
                health_script.SetScore(score);
                health_script.SetEnnemiKill(ennemiKill);
            }
            else
            {
                GameObject ennemi = EnnemiDist;
                GameObject newEnnemi = Instantiate(ennemi, new Vector3(Random.Range(1.6f, 3.3f), 1.62f, 0), Quaternion.identity);
                EnnemiDist ennemi_script = newEnnemi.GetComponentInChildren<EnnemiDist>();
                ennemi_script.SetwayPoints(PointGauche, PointDroit);
                Health health_script = newEnnemi.GetComponentInChildren<Health>();
                health_script.SetCompteRebours(compteRebours);
                health_script.SetSpawnerHaut(this.gameObject.GetComponent<SpawnerHaut>());
                health_script.SetScore(score);
                health_script.SetEnnemiKill(ennemiKill);
                LancerEclair lancer_script = newEnnemi.GetComponentInChildren<LancerEclair>();
                lancer_script.SetPlayer(player);
            }
            compteur = compteur + 1;
        }
        if (compteur == 0)
        {
            yield return new WaitForSeconds(30f);
        }
        StartCoroutine(spawnEnnemi(interval));
    }
}
