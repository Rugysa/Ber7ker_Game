using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private CompteRebours compteRebours;
    [SerializeField]
    private float tpsSup;
    [SerializeField]
    private SpawnerDroite spawnerDroite;
    private SpawnerBas spawnerBas;
    private SpawnerHaut spawnerHaut;
    [SerializeField] private Score score;
    private EnnemiKill ennemiKill;

    [SerializeField] private int soin = 35;
    [SerializeField] private GameObject heart;


    public HealthBar healthBar;
    private float timer;

    private int MAX_HEALTH = 100;

    public void SetCompteRebours(CompteRebours cr)
    {
        compteRebours = cr;
    }

    public void SetSpawnerDroite(SpawnerDroite spawner)
    {
        spawnerDroite = spawner;
    }

    public void SetSpawnerHaut(SpawnerHaut spawner)
    {
        spawnerHaut = spawner;
    }
    public void SetSpawnerBas(SpawnerBas spawner)
    {
        spawnerBas = spawner;
    }

    public void SetScore (Score score)
    {
        this.score = score;
    }

    public void SetEnnemiKill(EnnemiKill ennemi)
    {
        this.ennemiKill = ennemi;
    }
    public void Start()
    {
       timer = Time.timeSinceLevelLoad;
        int vie = MAX_HEALTH + ((int)timer / 60) * 20;
       this.health = vie;
       healthBar.SetMaxHealth(vie);
    }

    void Update()
    {
        // Pour le test
       /* if (Input.GetKeyUp(KeyCode.D))
        {
            Damage(10);
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            Heal(10);
        }*/
    }

    public void Damage(int damage)
    {

        if(damage < 0)
        {
            throw new System.ArgumentOutOfRangeException("Dommage négatif");
        }

        this.health -= damage; 
       healthBar.SetHealth(health);

        if(this.health <= 0)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {
        if(heal < 0)
        {
            throw new System.ArgumentOutOfRangeException("Soin négatif");
        }

        if(health + heal > MAX_HEALTH)
        {
            this.health = MAX_HEALTH;
        }

        else
        {
            this.health += heal;
        }
        healthBar.SetHealth(health);

    }

    public void Die()
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Player"))
        {
            int tps_jeu = (int)Time.timeSinceLevelLoad;
            score.GetComponent<Score>().SetScore(tps_jeu-1);
           GameOverManager.Instance.OnPLayerDeath();
        }
        if (gameObject.CompareTag("Ennemie"))
        {
            compteRebours.GetComponent<CompteRebours>().SetTime(tpsSup);
            if(gameObject.transform.position.y < 0f && gameObject.transform.position.y > -0.2f)
            {
                spawnerDroite.GetComponent<SpawnerDroite>().DecrementeCompteur();
            }
            if (gameObject.transform.position.y < -1f && gameObject.transform.position.y > -2f)
            {
                spawnerBas.GetComponent<SpawnerBas>().DecrementeCompteur();
            }
            if (gameObject.transform.position.y < 1.7f && gameObject.transform.position.y > 1.5f)
            {
                spawnerHaut.GetComponent<SpawnerHaut>().DecrementeCompteur();
            }

            score.GetComponent<Score>().SetScore(100);
            ennemiKill.GetComponent<EnnemiKill>().SetNbEnnemi();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("heal"))
        {
            Heal(soin);
            heart.SetActive(false);

        }
    }
}
