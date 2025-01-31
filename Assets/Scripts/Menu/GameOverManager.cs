using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameoverMenu;

    public static GameOverManager Instance;

    private Animator anim_rejouer;
    private Animator anim_menu;
    private Animator anim_quitter;

    [SerializeField] private GameObject boutton_rejouer;
    [SerializeField] private GameObject boutton_menu;
    [SerializeField] private GameObject boutton_quitter;

    private void Awake()
    {
        if(Instance == null)
        {
            Debug.Log("il y a plus d'insatnce);");
        }
        Instance = this;
    }

    public void OnPLayerDeath()
    {
        gameoverMenu.SetActive(true);
    }

    private void Start()
    {
        Screen.fullScreen = true;
        anim_rejouer = boutton_rejouer.GetComponent<Button>().GetComponent<Animator>();
        anim_menu = boutton_menu.GetComponent<Button>().GetComponent<Animator>();
        anim_quitter = boutton_quitter.GetComponent<Button>().GetComponent<Animator>();
    }
    public void Jouer_anim()
    {
        anim_rejouer.SetTrigger("Pressed");
    }

    public void Menu_anim()
    {
        anim_menu.SetTrigger("Pressed");
    }

    public void Quitter()
    {
        anim_quitter.SetTrigger("Pressed");
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Entrée"))
        // if (Input.GetKeyDown(KeyCode.Return))
        {
            if (EventSystem.current.currentSelectedGameObject == boutton_rejouer)
            {
                Jouer_anim();
            }
            if (EventSystem.current.currentSelectedGameObject == boutton_menu)
            {
                Menu_anim();
            }
            if (EventSystem.current.currentSelectedGameObject == boutton_quitter)
            {
                Quitter();
            }
        }
    }
}
