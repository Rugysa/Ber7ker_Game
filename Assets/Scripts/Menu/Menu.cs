using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Menu : MonoBehaviour
{
    public GameObject CommandeWindow;
    public GameObject PrincipeWindow;

     private Animator anim_jouer;
     private Animator anim_commandes;
     private Animator anim_principe;
     private Animator anim_quitter;

    [SerializeField] private GameObject boutton_jouer;
    [SerializeField] private GameObject boutton_commandes;
    [SerializeField] private GameObject boutton_principe;
    [SerializeField] private GameObject boutton_quitter;
    [SerializeField] private GameObject boutton_fermerfenetre;
    [SerializeField] private GameObject boutton_fermerfenetreprincipe;

    private void Start()
    {
        Screen.fullScreen = true;
        this.gameObject.GetComponentInChildren<Button>().Select();
        anim_jouer = boutton_jouer.GetComponent<Button>().GetComponent<Animator>();
        anim_commandes = boutton_commandes.GetComponent<Button>().GetComponent<Animator>();
        anim_principe = boutton_principe.GetComponent<Button>().GetComponent<Animator>();
        anim_quitter = boutton_quitter.GetComponent<Button>().GetComponent<Animator>();
    }
    public void Jouer_Anim()
    {
        anim_jouer.SetTrigger("Pressed");
        
    }

    public void Commandes()
    {
        anim_commandes.SetTrigger("Pressed");
        anim_jouer.SetTrigger("Pressed");
        CommandeWindow.SetActive(true);
        CommandeWindow.GetComponentInChildren<Button>().Select();
    }
    public void Fermerfenetre()
    {
        CommandeWindow.SetActive(false);
        boutton_commandes.GetComponent<Button>().Select();

    }

    public void Principe()
    {
        anim_principe.SetTrigger("Pressed");
        PrincipeWindow.SetActive(true);
        boutton_fermerfenetreprincipe.GetComponent<Button>().Select();
    }
    public void FermerfenetrePrincipe()
    {
        PrincipeWindow.SetActive(false);
        boutton_principe.GetComponent<Button>().Select();

    }
    public void Quitter()
    {
        anim_quitter.SetTrigger("Pressed");
        Application.Quit();
    }

    public void Update()
    {
         //if (Input.GetButtonDown("Entrée"))
         if(Input.GetKeyDown(KeyCode.Joystick1Button6))
        //if (Input.GetKeyDown(KeyCode.Return))KeyCode.Joystick1Button0
        {
            if (EventSystem.current.currentSelectedGameObject == boutton_jouer)
            {
                Jouer_Anim();
            }
            if (EventSystem.current.currentSelectedGameObject == boutton_commandes)
            {
                Commandes();
               // Debug.Log("cmd");

            }
            if (EventSystem.current.currentSelectedGameObject == boutton_principe)
            {
                Principe();
            }
            if (EventSystem.current.currentSelectedGameObject == boutton_quitter)
            {
                Quitter();
            }
        }
         if (Input.GetButtonDown("Retour")) 
        //if (Input.GetKeyDown (KeyCode.B))
        {
            if (EventSystem.current.currentSelectedGameObject == boutton_fermerfenetre)
            {
                Fermerfenetre();
            }
            if (EventSystem.current.currentSelectedGameObject == boutton_fermerfenetreprincipe)
            {
                FermerfenetrePrincipe();
            }
        }
    }
}

